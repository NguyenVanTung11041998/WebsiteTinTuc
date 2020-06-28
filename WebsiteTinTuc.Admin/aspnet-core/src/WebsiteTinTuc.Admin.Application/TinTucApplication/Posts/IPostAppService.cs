using System.Threading.Tasks;
using WebsiteTinTuc.Admin.TinTucApplication.Posts.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Posts
{
    public interface IPostAppService
    {
        Task CreateOrEditAsync(PostRequest input);
    }
}
