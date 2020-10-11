using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Authorization;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Helpers;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.BranchJobs.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.BranchJobs
{
    [AbpAuthorize]
    public class BranchJobAppService : AdminAppServiceBase, IBranchJobAppService
    {
        [AbpAuthorize(PermissionNames.Pages_Create_BranchJob)]
        public async Task CreateBranchJobAsync(BranchJobRequest input)
        {
            await SaveBranchJobAsync(input);
        }

        [AbpAuthorize(PermissionNames.Pages_Delete_BranchJob)]
        public async Task DeleteAsync(Guid id)
        {
            var branchJob = await WorkScope.GetAll<BranchJob>().FirstOrDefaultAsync(x => x.Id == id);
            if (branchJob == default)
                throw new UserFriendlyException("Ngành nghề không tồn tại");

            await WorkScope.DeleteAsync(branchJob);
        }

        private async Task SaveBranchJobAsync(BranchJobRequest input)
        {
            bool checkExists = await WorkScope.GetAll<BranchJob>().AnyAsync(x => x.Name == input.Name && x.Id != input.Id);
            if (checkExists)
                throw new UserFriendlyException("Ngành nghề đã tồn tại");

            string branchJobUrl = input.Name.RemoveSign4VietnameseString().ToIdentifier();
            if (input.Id.HasValue)
            {
                var branchJob = await WorkScope.GetAll<BranchJob>().FirstOrDefaultAsync(x => x.Id == input.Id);
                if (branchJob == default)
                    throw new UserFriendlyException("Ngành nghề không tồn tại");

                branchJob.Name = input.Name;
                branchJob.BranchJobUrl = branchJobUrl;
                await WorkScope.UpdateAsync(branchJob);
            }
            else
            {
                var hashtag = new BranchJob
                {
                    Name = input.Name,
                    BranchJobUrl = branchJobUrl
                };
                await WorkScope.InsertAsync(hashtag);
            }
        }

        [AbpAuthorize(PermissionNames.Pages_View_BranchJob)]
        public async Task<PagedResultDto<BranchJobModel>> GetAllBranchJobPagingAsync(PageRequest input)
        {
            var query = WorkScope.GetAll<BranchJob>()
                                      .WhereIf(!input.SearchText.IsNullOrWhiteSpace(), x => x.Name.Contains(input.SearchText))
                                      .Select(x => new BranchJobModel
                                      {
                                          BranchJobUrl = x.BranchJobUrl,
                                          Id = x.Id,
                                          Name = x.Name,
                                          CreationTime = x.CreationTime
                                      });
            int totalCount = await query.CountAsync();
            var branchJobs = await query.PageBy((input.CurrentPage - 1) * input.PageSize, input.PageSize).ToListAsync();
            return new PagedResultDto<BranchJobModel>(totalCount, branchJobs);
        }

        [AbpAllowAnonymous]
        public async Task<List<BranchJobModel>> GetAllBranchJobs()
        {
            return await WorkScope.GetAll<BranchJob>()
                        .Select(x => new BranchJobModel
                        {
                            BranchJobUrl = x.BranchJobUrl,
                            Id = x.Id,
                            Name = x.Name,
                            CreationTime = x.CreationTime
                        }).ToListAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_View_BranchJob)]
        public async Task<BranchJobModel> GetBranchJobByIdAsync(Guid id)
        {
            var branchJob = await WorkScope.GetAll<BranchJob>().Select(x => new BranchJobModel
            {
                Id = x.Id,
                BranchJobUrl = x.BranchJobUrl,
                Name = x.Name
            }).FirstOrDefaultAsync(x => x.Id == id);
            if (branchJob == default)
                throw new UserFriendlyException("Ngành nghề không tồn tại");

            return branchJob;
        }

        [AbpAuthorize(PermissionNames.Pages_Update_BranchJob)]
        public async Task UpdateBranchJobAsync(BranchJobRequest input)
        {
            await SaveBranchJobAsync(input);
        }
    }
}
