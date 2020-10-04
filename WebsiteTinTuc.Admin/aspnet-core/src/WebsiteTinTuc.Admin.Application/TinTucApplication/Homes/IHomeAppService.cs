using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.TinTucApplication.Homes.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Homes
{
    public interface IHomeAppService
    {
        Task<PagedResultDto<CompanyPostModel>> GetAllCompanyPostPaging(HomeFilter input);
        Task<List<CompanyPostModel>> GetTopNewPost(int count);
    }
}
