using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Http;
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
using WebsiteTinTuc.Admin.TinTucApplication.Agencies.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Agencies
{
    [AbpAuthorize]
    public class AgencyAppService : AdminAppServiceBase, IAgencyAppService
    {
        private async Task SaveImage(string fileLocation, Guid agencyId, IFormFile file)
        {
            string fileName = await UploadFiles.UploadAsync(fileLocation, file);
            var asset = new Asset
            {
                FileType = FileType.Image,
                Path = $"{ConstantVariable.UploadFolder}/{ConstantVariable.Agency}/{fileName}",
                AgencyId = agencyId
            };
            await WorkScope.InsertAsync(asset);
        }

        private async Task DeleteHashtags(Guid agencyId, List<Guid> hashtagIds)
        {
            var hashtags = await WorkScope.GetAll<AgencyPostHashtag>()
                                          .Where(x => hashtagIds.Contains(x.HashtagId) && x.AgencyId == agencyId)
                                          .ToListAsync();

            foreach (var item in hashtags)
            {
                await WorkScope.DeleteAsync(item);
            }
        }

        private async Task DeleteBranchJobAgencies(Guid agencyId, List<Guid> branchJobAgencyIds)
        {
            var branchJobAgencies = await WorkScope.GetAll<BranchJobAgency>()
                                          .Where(x => branchJobAgencyIds.Contains(x.BranchJobId) && x.AgencyId == agencyId)
                                          .ToListAsync();

            foreach (var item in branchJobAgencies)
            {
                await WorkScope.DeleteAsync(item);
            }
        }

        private async Task DeleteImages(Guid agencyId, List<Guid> imageIds)
        {
            var images = await WorkScope.GetAll<Asset>()
                                          .Where(x => imageIds.Contains(x.Id) && x.AgencyId == agencyId)
                                          .ToListAsync();

            foreach (var item in images)
            {
                await WorkScope.DeleteAsync(item);
            }
        }

        private async Task SaveImageHashtagAndAgencyJob(Guid agencyId, List<IFormFile> images, IFormFile thumbnail, IFormFile nationalityImage, List<Guid> hashtagIds, List<Guid> branchJobAgencyIds)
        {
            string fileLocation = UploadFiles.CreateFolderIfNotExists(ConstantVariable.RootFolder, $@"{ConstantVariable.UploadFolder}\{ConstantVariable.Agency}");

            if (images?.Count > 0)
            {
                foreach (var item in images)
                {
                    await SaveImage(fileLocation, agencyId, item);
                }
            }

            if (thumbnail?.Length > 0)
            {
                await SaveImage(fileLocation, agencyId, thumbnail);
            }

            if (nationalityImage?.Length > 0)
            {
                await SaveImage(fileLocation, agencyId, nationalityImage);
            }

            if (hashtagIds?.Count > 0)
            {
                foreach (var item in hashtagIds)
                {
                    var postHashtag = new AgencyPostHashtag
                    {
                        HashtagId = item,
                        AgencyId = agencyId
                    };
                    await WorkScope.InsertAsync(postHashtag);
                }
            }

            if (branchJobAgencyIds?.Count > 0)
            {
                foreach (var item in branchJobAgencyIds)
                {
                    var branchJobAgency = new BranchJobAgency
                    {
                        BranchJobId = item,
                        AgencyId = agencyId
                    };
                    await WorkScope.InsertAsync(branchJobAgency);
                }
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Create_Agency)]
        public async Task CreateAgencyAsync(AgencyRequest input)
        {
            bool checkExits = await WorkScope.GetAll<Agency>().AnyAsync(x => x.Name == input.Name);
            if (checkExits)
                throw new UserFriendlyException("Tên công ty đã tồn tại!");

            var agency = ObjectMapper.Map<Agency>(input);
            agency.AgencyUrl = agency.Name.RemoveSign4VietnameseString().ToIdentifier();
            agency.UserId = AbpSession.UserId.Value;

            input.Id = await WorkScope.InsertAndGetIdAsync(agency);

            await SaveImageHashtagAndAgencyJob(input.Id.Value, input.Files, input.Thumbnail, input.NationalityImage, input.HashtagIds, input.BranchJobAgencyIds);
        }

        [AbpAuthorize(PermissionNames.Pages_Delete_Agency)]
        public async Task DeleteAgencyAsync(Guid id)
        {
            var user = await GetCurrentUserAsync();
            var agency = await WorkScope.GetAll<Agency>()
                .WhereIf(user.UserType == UserType.Hr, x => x.UserId == user.Id)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (agency == default)
                throw new UserFriendlyException("Công ty không thể xóa");

            await WorkScope.DeleteAsync(agency);
        }

        [AbpAuthorize(PermissionNames.Pages_Update_Agency)]
        public async Task UpdateAgencyAsync(AgencyRequest input)
        {
            var user = await GetCurrentUserAsync();
            bool checkExits = await WorkScope.GetAll<Agency>()
                                .AnyAsync(x => x.Name == input.Name && x.Id != input.Id);
            if (checkExits)
                throw new UserFriendlyException("Tên công ty đã tồn tại!");

            var agency = await WorkScope.GetAll<Agency>()
                                .WhereIf(user.UserType == UserType.Hr, x => x.UserId == user.Id)
                                .FirstOrDefaultAsync(x => x.Id == input.Id);
            if (agency == default)
                throw new UserFriendlyException("Công ty không thể sửa");

            ObjectMapper.Map(input, agency);
            agency.AgencyUrl = agency.Name.RemoveSign4VietnameseString().ToIdentifier();

            foreach (var item in agency.Posts)
            {
                item.PostUrl = $"{item.Title.RemoveSign4VietnameseString().ToIdentifier()}-{agency.AgencyUrl}";
            }

            await WorkScope.UpdateRangeAsync(agency.Posts);
            await WorkScope.UpdateAsync(agency);
            await DeleteHashtags(agency.Id, input.HashtagDeletes);
            await DeleteBranchJobAgencies(agency.Id, input.BranchJobAgencyDeletes);
            await DeleteImages(agency.Id, input.ImageDeletes);
            await SaveImageHashtagAndAgencyJob(input.Id.Value, input.Files, input.Thumbnail, input.NationalityImage, input.HashtagIds, input.BranchJobAgencyIds);
        }

        [AbpAuthorize(PermissionNames.Pages_View_Agency)]
        public async Task<PagedResultDto<AgencyModel>> GetAgencyPagingAsync(AgencyFilterPaging input)
        {
            var user = await GetCurrentUserAsync();

            var query = WorkScope.GetAll<Agency>()
                                 .WhereIf(!input.SearchText.IsNullOrWhiteSpace(), x => x.Name.Contains(input.SearchText))
                                 .WhereIf(user.UserType == UserType.Hr, x => x.UserId == user.Id)
                                 .OrderByDescending(x => x.CreationTime);

            int totalCount = await query.CountAsync();
            var list = await query.PageBy((input.CurrentPage - 1) * input.PageSize, input.PageSize)
                .Select(x => new AgencyModel
                {
                    Name = x.Name,
                    CreationTime = x.CreationTime,
                    LocationDescription = x.LocationDescription,
                    Email = x.Email,
                    Phone = x.Phone,
                    Website = x.Website
                })
                .ToListAsync();
            return new PagedResultDto<AgencyModel>(totalCount, list);
        }

        [AbpAuthorize(PermissionNames.Pages_View_Agency)]
        public async Task<AgencyModel> GetAgencyByIdAsync(Guid id)
        {
            var user = await GetCurrentUserAsync();
            var agency = await WorkScope.GetAll<Agency>()
                            .Where(x => x.Id == id)
                            .WhereIf(user.UserType == UserType.Hr, x => x.UserId == user.Id)
                            .Include(x => x.AgencyPostHashtags)
                            .Include(x => x.Assets)
                            .Include(x => x.BranchJobAgencies)
                            .Select(x => new AgencyModel
                            {
                                Name = x.Name,
                                Description = x.Description,
                                DescrtionAgency = x.DescrtionAgency,
                                Email = x.Email,
                                Id = x.Id,
                                Location = x.Location,
                                LocationDescription = x.LocationDescription,
                                MaxScale = x.MaxScale,
                                MinScale = x.MinScale,
                                NationalityAgency = x.NationalityAgency,
                                Treatment = x.Treatment,
                                Website = x.Website,
                                Phone = x.Phone,
                                Thumbnail = x.Assets.Where(p => p.FileType == FileType.Thumbnail).Select(p => new ObjectFile
                                {
                                    FileType = p.FileType,
                                    Id = p.Id,
                                    Path = p.Path
                                }).FirstOrDefault(),
                                BranchJobAgencies = x.BranchJobAgencies.Select(p => new BranchJobAgencyModel
                                {
                                    Id = p.Id,
                                    BranchJobId = p.BranchJobId
                                }),
                                NationalityImage = x.Assets.Where(p => p.FileType == FileType.NationalityImage).Select(p => new ObjectFile
                                {
                                    FileType = p.FileType,
                                    Id = p.Id,
                                    Path = p.Path
                                }).FirstOrDefault(),
                                Images = x.Assets.Where(p => p.FileType == FileType.Image).Select(p => new ObjectFile
                                {
                                    FileType = p.FileType,
                                    Id = p.Id,
                                    Path = p.Path
                                }),
                                Hashtags = x.AgencyPostHashtags.Select(p => new HashtagAgencyModel
                                {
                                    Id = p.Id,
                                    HashtagId = p.HashtagId
                                })
                            }).FirstOrDefaultAsync();

            if (agency == default)
                throw new UserFriendlyException("Không tồn tại công ty");

            return agency;
        }
    }
}
