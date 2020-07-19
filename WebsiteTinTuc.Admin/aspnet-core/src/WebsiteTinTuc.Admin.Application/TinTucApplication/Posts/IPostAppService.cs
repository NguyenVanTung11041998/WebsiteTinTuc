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
        Task CreateOrEditAsync(PostRequest input);
        Task<PagedResultDto<PostDto>> GetAllPostPagingAsync(PageRequest input);
        Task<string> UploadImage([FromForm]IFormFile file);
        Task<PostDto> GetPostByIdAsync(Guid id);
    }
}
