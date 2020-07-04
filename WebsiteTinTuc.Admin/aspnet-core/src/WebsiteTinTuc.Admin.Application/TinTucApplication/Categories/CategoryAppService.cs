using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Helpers;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.Categories.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Categories
{
    [AbpAuthorize]
    public class CategoryAppService : AdminAppServiceBase, ICategoryAppService
    {
        public async Task CreateOrEditAsync(CategoryRequest input)
        {
            bool checkExits = await WorkScope.GetAll<Category>().AnyAsync(x => x.Name == input.Name && x.Id != input.Id);
            if (checkExits)
                throw new UserFriendlyException("Tên danh mục đã tồn tại!");

            if (input.Id.HasValue)
            {
                var category = await WorkScope.GetAll<Category>().FirstOrDefaultAsync(x => x.Id == input.Id);
                if (category == default)
                    throw new UserFriendlyException("Danh mục không tồn tại");

                category.Name = input.Name;
                category.CategoryUrl = input.Name.RemoveSign4VietnameseString().ToIdentifier();
                await WorkScope.UpdateAsync(category);
            }
            else
            {
                var category = new Category
                {
                    Name = input.Name,
                    CategoryUrl = input.Name.RemoveSign4VietnameseString().ToIdentifier()
                };
                await WorkScope.InsertAsync(category);
            }
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
        {
            return await WorkScope.GetAll<Category>()
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    CategoryUrl = x.CategoryUrl
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await WorkScope.GetAll<Category>().FirstOrDefaultAsync(x => x.Id == id);
            if (category == default)
                throw new UserFriendlyException("Danh mục không tồn tại");
            await WorkScope.DeleteAsync(category);
        }

        public async Task<PagedResultDto<CategoryDto>> GetAllCategoryPagingAsync(PageRequest input)
        {
            var queryCategory = WorkScope.GetAll<Category>()
                                      .WhereIf(!input.SearchText.IsNullOrWhiteSpace(), x => x.Name.Contains(input.SearchText))
                                      .Select(x => new CategoryDto
                                      {
                                          CategoryUrl = x.CategoryUrl,
                                          Id = x.Id,
                                          Name = x.Name,
                                          CreationTime = x.CreationTime
                                      });
            int totalCount = await queryCategory.CountAsync();
            var categories = await queryCategory.Skip((input.CurrentPage - 1) * input.PageSize).Take(input.PageSize).ToListAsync();
            return new PagedResultDto<CategoryDto>(totalCount, categories);
        }

        public async Task<List<CategoryDto>> GetAllCategoryAsync()
        {
            return await WorkScope.GetAll<Category>()
                                    .Select(x => new CategoryDto
                                    {
                                        CategoryUrl = x.CategoryUrl,
                                        Id = x.Id,
                                        Name = x.Name,
                                        CreationTime = x.CreationTime
                                    }).ToListAsync();
        }
    }
}
