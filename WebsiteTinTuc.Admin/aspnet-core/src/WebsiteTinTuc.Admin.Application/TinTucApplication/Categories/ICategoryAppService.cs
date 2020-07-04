using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.Categories.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Categories
{
    public interface ICategoryAppService
    {
        Task CreateOrEditAsync(CategoryRequest input);
        Task<CategoryDto> GetCategoryByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<PagedResultDto<CategoryDto>> GetAllCategoryPagingAsync(PageRequest input);
        Task<List<CategoryDto>> GetAllCategoryAsync();
    }
}
