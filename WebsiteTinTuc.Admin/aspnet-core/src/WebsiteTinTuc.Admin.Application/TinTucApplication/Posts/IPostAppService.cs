using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.TinTucApplication.Posts.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Posts
{
    public interface IPostAppService
    {
        Task CreatePostAsync(PostRequest input);
        Task EditPostAsync(PostRequest input);
        Task<PagedResultDto<PostDto>> GetAllPostPagingAsync(PostPagingRequest input);
        Task<PostDto> GetPostByIdAsync(Guid id);
        Task DeletePostAsync(Guid id);
        Task<List<PostTopProminent>> GetTopPostProminent(int? count = null);
    }
}
