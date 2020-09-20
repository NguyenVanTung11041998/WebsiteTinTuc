using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.TinTucApplication.Homes.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Homes
{
    public interface IHomeAppService
    {
        Task<PagedResultDto<CompanyPostModel>> GetAllCompanyPostPaging(HomeFilter input);
    }
}
