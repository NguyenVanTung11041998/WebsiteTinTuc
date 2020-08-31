using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebsiteTinTuc.Admin.TinTucApplication.Levels
{
    public interface ILevelAppService
    {
        Task CreateHashtagAsync(HashtagRequest input);
        Task UpdateHashtagAsync(HashtagRequest input);
        Task<HashtagModel> GetHashtagByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<PagedResultDto<HashtagModel>> GetAllHashtagPagingAsync(PageRequest input);
        Task<List<HashtagModel>> GetAllHashtags();
    }
}
