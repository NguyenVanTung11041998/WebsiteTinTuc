using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.BranchJobs.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.BranchJobs
{
    public interface IBranchJobAppService
    {
        Task CreateBranchJobAsync(BranchJobRequest input);
        Task UpdateBranchJobAsync(BranchJobRequest input);
        Task<BranchJobModel> GetBranchJobByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<PagedResultDto<BranchJobModel>> GetAllBranchJobPagingAsync(PageRequest input);
        Task<List<BranchJobModel>> GetAllBranchJobs();
    }
}
