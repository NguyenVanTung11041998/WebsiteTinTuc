using System.Threading.Tasks;
using Abp.Application.Services;
using WebsiteTinTuc.Admin.Authorization.Accounts.Dto;

namespace WebsiteTinTuc.Admin.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
