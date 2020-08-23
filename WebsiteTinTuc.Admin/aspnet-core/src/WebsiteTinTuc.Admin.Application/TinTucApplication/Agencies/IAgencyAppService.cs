using Abp.Application.Services.Dto;
using System;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.TinTucApplication.Agencies.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Agencies
{
    public interface IAgencyAppService
    {
        Task CreateAgencyAsync(AgencyRequest input);
        Task UpdateAgencyAsync(AgencyRequest input);
        Task DeleteAgencyAsync(Guid id);
        Task<PagedResultDto<AgencyModel>> GetAgencyPagingAsync(AgencyFilterPaging input);
        Task<AgencyModel> GetAgencyByIdAsync(Guid id);
    }
}
