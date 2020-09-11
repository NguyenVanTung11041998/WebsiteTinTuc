using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.Posts.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Posts
{
    public interface IPostAppService
    {
        Task CreatePostAsync(PostRequest input);
        Task EditPostAsync(PostRequest input);
        Task<PagedResultDto<PostDto>> GetAllPostPagingAsync(PageRequest input);
        Task<PostDto> GetPostByIdAsync(Guid id);
        Task DeletePostAsync(Guid id);
    }
}
