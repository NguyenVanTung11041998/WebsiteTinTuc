using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.Posts.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Posts
{
    public interface IPostAppService
    {
        Task CreateOrEditAsync(PostRequest input);
        Task<PagedResultDto<PostDto>> GetAllPostPagingAsync(PageRequest input);
    }
}
