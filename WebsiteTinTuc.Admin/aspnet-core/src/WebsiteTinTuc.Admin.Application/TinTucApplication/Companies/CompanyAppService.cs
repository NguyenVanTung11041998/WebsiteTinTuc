using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Authorization;
using WebsiteTinTuc.Admin.Authorization.Users;
using WebsiteTinTuc.Admin.Constans;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Helpers;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.Companies.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Companies
{
    [AbpAuthorize]
    public class CompanyAppService : AdminAppServiceBase, ICompanyAppService
    {
        private async Task SaveImage(string fileLocation, Guid companyId, IFormFile file, FileType fileType = FileType.Image)
        {
            string fileName = await UploadFiles.UploadAsync(fileLocation, file);
            var asset = new Asset
            {
                FileType = fileType,
                Path = $"{ConstantVariable.UploadFolder}/{ConstantVariable.Company}/{fileName}",
                CompanyId = companyId
            };
            await WorkScope.InsertAsync(asset);
        }

        private async Task DeleteHashtags(Guid companyId, List<Guid> hashtagIds)
        {
            var hashtags = await WorkScope.GetAll<CompanyPostHashtag>()
                                          .Where(x => hashtagIds.Contains(x.Id))
                                          .ToListAsync();

            foreach (var item in hashtags)
            {
                await WorkScope.DeleteAsync(item);
            }
        }

        private async Task DeleteBranchJobCompanies(Guid companyId, List<Guid> branchJobCompanyIds)
        {
            var branchJobCompanies = await WorkScope.GetAll<BranchJobCompany>()
                                          .Where(x => branchJobCompanyIds.Contains(x.Id))
                                          .ToListAsync();

            foreach (var item in branchJobCompanies)
            {
                await WorkScope.DeleteAsync(item);
            }
        }

        private async Task DeleteImages(Guid companyId, List<Guid> imageIds)
        {
            var images = await WorkScope.GetAll<Asset>()
                                          .Where(x => imageIds.Contains(x.Id) && x.CompanyId == companyId)
                                          .ToListAsync();

            foreach (var item in images)
            {
                await WorkScope.DeleteAsync(item);
            }
        }

        private async Task SaveImageHashtagAndCompanyJob(Guid companyId, List<IFormFile> images, IFormFile thumbnail, List<Guid> hashtagIds, List<Guid> branchJobCompanyIds)
        {
            string fileLocation = UploadFiles.CreateFolderIfNotExists(ConstantVariable.RootFolder, $@"{ConstantVariable.UploadFolder}\{ConstantVariable.Company}");

            if (images?.Count > 0)
            {
                foreach (var item in images)
                {
                    await SaveImage(fileLocation, companyId, item);
                }
            }

            if (thumbnail?.Length > 0)
            {
                await SaveImage(fileLocation, companyId, thumbnail, FileType.Thumbnail);
            }

            if (hashtagIds?.Count > 0)
            {
                foreach (var item in hashtagIds)
                {
                    var postHashtag = new CompanyPostHashtag
                    {
                        HashtagId = item,
                        CompanyId = companyId
                    };
                    await WorkScope.InsertAsync(postHashtag);
                }
            }

            if (branchJobCompanyIds?.Count > 0)
            {
                foreach (var item in branchJobCompanyIds)
                {
                    var branchJobCompany = new BranchJobCompany
                    {
                        BranchJobId = item,
                        CompanyId = companyId
                    };
                    await WorkScope.InsertAsync(branchJobCompany);
                }
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Create_Company)]
        public async Task CreateCompanyAsync([FromForm] CompanyRequest input)
        {
            bool checkExits = await WorkScope.GetAll<Company>().AnyAsync(x => x.Name == input.Name);
            if (checkExits)
                throw new UserFriendlyException("Tên công ty đã tồn tại!");

            var company = ObjectMapper.Map<Company>(input);
            company.CompanyUrl = company.Name.RemoveSign4VietnameseString().ToIdentifier();
            company.UserId = AbpSession.UserId.Value;

            input.Id = await WorkScope.InsertAndGetIdAsync(company);

            await SaveImageHashtagAndCompanyJob(input.Id.Value, input.Files, input.Thumbnail, input.HashtagIds, input.BranchJobCompanyIds);
        }

        [AbpAuthorize(PermissionNames.Pages_Delete_Company)]
        public async Task DeleteCompanyAsync(Guid id)
        {
            var user = await GetCurrentUserAsync();
            var company = await WorkScope.GetAll<Company>()
                .WhereIf(user.UserType == UserType.Hr, x => x.UserId == user.Id)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (company == default)
                throw new UserFriendlyException("Công ty không thể xóa");

            await WorkScope.DeleteAsync(company);
        }

        [AbpAuthorize(PermissionNames.Pages_Update_Company)]
        public async Task UpdateCompanyAsync([FromForm] CompanyRequest input)
        {
            var user = await GetCurrentUserAsync();
            bool checkExits = await WorkScope.GetAll<Company>()
                                .AnyAsync(x => x.Name == input.Name && x.Id != input.Id);
            if (checkExits)
                throw new UserFriendlyException("Tên công ty đã tồn tại!");

            var company = await WorkScope.GetAll<Company>()
                                .WhereIf(user.UserType == UserType.Hr, x => x.UserId == user.Id)
                                .Include(x => x.Posts)
                                .FirstOrDefaultAsync(x => x.Id == input.Id);
            if (company == default)
                throw new UserFriendlyException("Công ty không thể sửa");

            ObjectMapper.Map(input, company);
            company.CompanyUrl = company.Name.RemoveSign4VietnameseString().ToIdentifier();

            foreach (var item in company.Posts)
            {
                item.PostUrl = $"{item.Title.RemoveSign4VietnameseString().ToIdentifier()}-{company.CompanyUrl}";
            }

            await WorkScope.UpdateRangeAsync(company.Posts);
            await WorkScope.UpdateAsync(company);

            if (input.HashtagDeletes != null)
                await DeleteHashtags(company.Id, input.HashtagDeletes);

            if (input.BranchJobCompanyDeletes != null)
                await DeleteBranchJobCompanies(company.Id, input.BranchJobCompanyDeletes);

            if (input.ImageDeletes != null)
                await DeleteImages(company.Id, input.ImageDeletes);

            await SaveImageHashtagAndCompanyJob(input.Id.Value, input.Files, input.Thumbnail, input.HashtagIds, input.BranchJobCompanyIds);
        }

        [AbpAuthorize(PermissionNames.Pages_View_Company)]
        public async Task<PagedResultDto<CompanyModel>> GetCompanyPagingAsync(CompanyFilterPaging input)
        {
            var user = await GetCurrentUserAsync();

            var query = WorkScope.GetAll<Company>()
                                 .WhereIf(!input.SearchText.IsNullOrWhiteSpace(), x => x.Name.Contains(input.SearchText))
                                 .WhereIf(user.UserType == UserType.Hr, x => x.UserId == user.Id)
                                 .OrderByDescending(x => x.CreationTime);

            int totalCount = await query.CountAsync();
            var list = await query.PageBy((input.CurrentPage - 1) * input.PageSize, input.PageSize)
                .Select(x => new CompanyModel
                {
                    Name = x.Name,
                    FullNameCompany = x.FullNameCompany,
                    CreationTime = x.CreationTime,
                    Id = x.Id,
                    LocationDescription = x.LocationDescription
                }).ToListAsync();
            return new PagedResultDto<CompanyModel>(totalCount, list);
        }

        [AbpAuthorize(PermissionNames.Pages_View_Company)]
        public async Task<CompanyModel> GetCompanyByIdAsync(Guid id)
        {
            var user = await GetCurrentUserAsync();
            var company = await WorkScope.GetAll<Company>()
                            .Where(x => x.Id == id)
                            .WhereIf(user.UserType == UserType.Hr, x => x.UserId == user.Id)
                            .Include(x => x.CompanyPostHashtags)
                            .Include(x => x.Assets)
                            .Include(x => x.BranchJobCompanies)
                            .Include(x => x.Nationality)
                            .Select(x => new CompanyModel
                            {
                                Name = x.Name,
                                Description = x.Description,
                                FullNameCompany = x.FullNameCompany,
                                Email = x.Email,
                                Id = x.Id,
                                Location = x.Location,
                                LocationDescription = x.LocationDescription,
                                MaxScale = x.MaxScale,
                                MinScale = x.MinScale,
                                NationalityCompany = x.Nationality.Name,
                                Treatment = x.Treatment,
                                Website = x.Website,
                                Phone = x.Phone,
                                Thumbnail = x.Assets.Where(p => p.FileType == FileType.Thumbnail && p.CompanyId == id).Select(p => new ObjectFile
                                {
                                    FileType = p.FileType,
                                    Id = p.Id,
                                    Path = p.Path
                                }).FirstOrDefault(),
                                BranchJobCompanies = x.BranchJobCompanies.Select(p => new BranchJobCompanyModel
                                {
                                    Id = p.Id,
                                    BranchJobId = p.BranchJobId
                                }),
                                Images = x.Assets.Where(p => p.FileType == FileType.Image && p.CompanyId == id).Select(p => new ObjectFile
                                {
                                    FileType = p.FileType,
                                    Id = p.Id,
                                    Path = p.Path
                                }),
                                Hashtags = x.CompanyPostHashtags.Select(p => new HashtagCompanyModel
                                {
                                    Id = p.Id,
                                    HashtagId = p.HashtagId
                                }),
                                NationalityId = x.NationalityId
                            }).FirstOrDefaultAsync();

            if (company == default)
                throw new UserFriendlyException("Không tồn tại công ty");

            return company;
        }
    }
}
