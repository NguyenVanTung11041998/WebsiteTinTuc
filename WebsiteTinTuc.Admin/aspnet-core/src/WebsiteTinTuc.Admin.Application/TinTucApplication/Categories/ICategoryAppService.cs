using System.Threading.Tasks;
using WebsiteTinTuc.Admin.TinTucApplication.Categories.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Categories
{
    public interface ICategoryAppService
    {
        Task CreateOrEditAsync(CategoryRequest input);
    }
}
