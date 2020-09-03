using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.Levels.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Levels
{
    public interface ILevelAppService
    {
        Task CreateLevelAsync(LevelRequest input);
        Task UpdateLevelAsync(LevelRequest input);
        Task<LevelModel> GetLevelByIdAsync(Guid id);
        Task DeleteLevelAsync(Guid id);
        Task<PagedResultDto<LevelModel>> GetAllLevelPagingAsync(PageRequest input);
        Task<List<LevelModel>> GetAllLevels();
    }
}
