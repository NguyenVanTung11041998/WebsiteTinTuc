using System.Threading.Tasks;
using Abp.Application.Services;
using WebsiteTinTuc.Admin.Sessions.Dto;

namespace WebsiteTinTuc.Admin.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
