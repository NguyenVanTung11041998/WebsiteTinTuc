using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Helpers;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.BranchJobs.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.BranchJobs
{
    public class BranchJobAppServiceApplication : AdminAppServiceBase, IBranchJobAppService
    {
        public async Task CreateBranchJobAsync(BranchJobRequest input)
        {
            await SaveBranchJobAsync(input);
        }

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

            if (input.Id.HasValue)
            {
                var branchJob = await WorkScope.GetAll<BranchJob>().FirstOrDefaultAsync(x => x.Id == input.Id);
                if (branchJob == default)
                    throw new UserFriendlyException("Ngành nghề không tồn tại");

                branchJob.Name = input.Name;
                branchJob.BranchJobUrl = input.Name.RemoveSign4VietnameseString().ToIdentifier();
                await WorkScope.UpdateAsync(branchJob);
            }
            else
            {
                var hashtag = new BranchJob
                {
                    Name = input.Name,
                    BranchJobUrl = input.Name.RemoveSign4VietnameseString().ToIdentifier()
                };
                await WorkScope.InsertAsync(hashtag);
            }
        }

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

        public async Task UpdateBranchJobAsync(BranchJobRequest input)
        {
            await SaveBranchJobAsync(input);
        }
    }
}
