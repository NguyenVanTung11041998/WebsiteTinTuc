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
using WebsiteTinTuc.Admin.Authorization;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.Levels.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Levels
{
    [AbpAuthorize]
    public class LevelAppService : AdminAppServiceBase, ILevelAppService
    {
        private async Task SaveLevelAsync(LevelRequest input)
        {
            bool checkExists = await WorkScope.GetAll<Level>().AnyAsync(x => x.Name == input.Name && x.Id != input.Id);
            if (checkExists)
                throw new UserFriendlyException("Level đã tồn tại");

            if (input.Id.HasValue)
            {
                var level = await WorkScope.GetAll<Level>().FirstOrDefaultAsync(x => x.Id == input.Id);
                if (level == default)
                    throw new UserFriendlyException("Level không tồn tại");

                level.Name = input.Name;
                await WorkScope.UpdateAsync(level);
            }
            else
            {
                var level = new Level
                {
                    Name = input.Name
                };
                await WorkScope.InsertAsync(level);
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Create_Level)]
        public async Task CreateLevelAsync(LevelRequest input)
        {
            await SaveLevelAsync(input);
        }

        [AbpAuthorize(PermissionNames.Pages_Delete_Level)]
        public async Task DeleteLevelAsync(Guid id)
        {
            var level = await WorkScope.GetAll<Level>().FirstOrDefaultAsync(x => x.Id == id);
            if (level == default)
                throw new UserFriendlyException("Level không tồn tại");

            await WorkScope.DeleteAsync(level);
        }

        [AbpAuthorize(PermissionNames.Pages_View_Level)]
        public async Task<List<LevelModel>> GetAllLevels()
        {
            return await WorkScope.GetAll<Level>()
                        .Select(x => new LevelModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            CreationTime = x.CreationTime
                        }).ToListAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_View_Level)]
        public async Task<PagedResultDto<LevelModel>> GetAllLevelPagingAsync(PageRequest input)
        {
            var query = WorkScope.GetAll<Level>()
                                      .WhereIf(!input.SearchText.IsNullOrWhiteSpace(), x => x.Name.Contains(input.SearchText))
                                      .Select(x => new LevelModel
                                      {
                                          Id = x.Id,
                                          Name = x.Name,
                                          CreationTime = x.CreationTime
                                      });
            int totalCount = await query.CountAsync();
            var levels = await query.PageBy((input.CurrentPage - 1) * input.PageSize, input.PageSize).ToListAsync();
            return new PagedResultDto<LevelModel>(totalCount, levels);
        }

        [AbpAuthorize(PermissionNames.Pages_View_Level)]
        public async Task<LevelModel> GetLevelByIdAsync(Guid id)
        {
            var level = await WorkScope.GetAll<Level>().Select(x => new LevelModel
            {
                Id = x.Id,
                Name = x.Name
            }).FirstOrDefaultAsync(x => x.Id == id);
            if (level == default)
                throw new UserFriendlyException("Level không tồn tại");

            return level;
        }

        [AbpAuthorize(PermissionNames.Pages_Update_Level)]
        public async Task UpdateLevelAsync(LevelRequest input)
        {
            await SaveLevelAsync(input);
        }
    }
}
