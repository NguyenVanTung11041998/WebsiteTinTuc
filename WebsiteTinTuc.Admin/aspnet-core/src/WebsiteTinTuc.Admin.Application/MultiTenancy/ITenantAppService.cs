using Abp.Application.Services;
using WebsiteTinTuc.Admin.MultiTenancy.Dto;

namespace WebsiteTinTuc.Admin.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

