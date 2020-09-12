using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.Hashtags.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Hashtags
{
    public interface IHashtagAppService
    {
        Task CreateHashtagAsync(HashtagRequest input);
        Task UpdateHashtagAsync(HashtagRequest input);
        Task<HashtagModel> GetHashtagByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<PagedResultDto<HashtagModel>> GetAllHashtagPagingAsync(PageRequest input);
        Task<List<HashtagModel>> GetAllHashtags();
        Task<List<HashtagModel>> GetAllHashtagIsHot();
    }
}
