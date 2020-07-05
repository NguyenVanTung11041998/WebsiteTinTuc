using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Constans;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Helpers;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.Posts.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Posts
{
    [AbpAuthorize]
    public class PostAppService : AdminAppServiceBase, IPostAppService
    {
        [RequestSizeLimit(1000_000_000)]
        public async Task CreateOrEditAsync([FromForm]PostRequest input)
        {
            bool checkExits = await WorkScope.GetAll<Post>().AnyAsync(x => x.Title == input.Title && x.Id != input.Id);
            if (checkExits)
                throw new UserFriendlyException("Tiêu đề bài viết đã tồn tại!");

            var fileLocation = UploadFiles.CreateFolderIfNotExists(ConstantVariable.RootFolder, $@"{ConstantVariable.UploadFolder}\{ConstantVariable.PostFolder}");
            if (input.Id.HasValue)
            {
                var post = await WorkScope.GetAll<Post>().FirstOrDefaultAsync(x => x.Id == input.Id);
                if (post == default)
                    throw new UserFriendlyException("Post không tồn tại");

                post.PostUrl = input.Title.RemoveSign4VietnameseString().ToIdentifier();
                post.Title = input.Title;
                post.Description = input.Description;
                post.Content = input.Content;
                await WorkScope.UpdateAsync(post);

                if (input.CategoryIdDelete?.Count > 0)
                {
                    foreach (var item in input.CategoryIdDelete)
                    {
                        await WorkScope.DeleteAsync<PostCategory>(item);
                    }
                }

                if (input.FileIdDelete?.Count > 0)
                {
                    foreach (var item in input.FileIdDelete)
                    {
                        await WorkScope.DeleteAsync<Asset>(item);
                    }
                }

                if (input.HashtagIdDelete?.Count > 0)
                {
                    foreach (var item in input.HashtagIdDelete)
                    {
                        await WorkScope.DeleteAsync<PostHashtag>(item);
                    }
                }
            }
            else
            {
                var post = new Post
                {
                    Description = input.Description,
                    Content = input.Content,
                    NumberOfComments = 0,
                    NumberOfLikes = 0,
                    NumberOfViews = 0,
                    PostUrl = input.Title.RemoveSign4VietnameseString().ToIdentifier(),
                    Title = input.Title
                };
                input.Id = await WorkScope.InsertAndGetIdAsync(post);
            }

            if (input.Files?.Count > 0)
            {
                foreach (var item in input.Files)
                {
                    string fileName = await UploadFiles.UploadAsync(fileLocation, item.File);
                    var asset = new Asset
                    {
                        FileType = item.FileType,
                        Path = $"{ConstantVariable.UploadFolder}/{ConstantVariable.PostFolder}/{fileName}",
                        PostId = input.Id.Value
                    };
                    await WorkScope.InsertAsync(asset);
                }
            }

            if (input.HashtagIds?.Count > 0)
            {
                foreach (var item in input.HashtagIds)
                {
                    var postHashtag = new PostHashtag
                    {
                        HashtagId = item,
                        PostId = input.Id.Value
                    };
                    await WorkScope.InsertAsync(postHashtag);
                }
            }

            if (input.CategoryIds?.Count > 0)
            {
                foreach (var item in input.CategoryIds)
                {
                    var postCategory = new PostCategory
                    {
                        CategoryId = item,
                        PostId = input.Id.Value
                    };
                    await WorkScope.InsertAsync(postCategory);
                }
            }
        }

        public async Task<PagedResultDto<PostDto>> GetAllPostPagingAsync(PageRequest input)
        {
            var queryPost = WorkScope.GetAll<Post>()
                .WhereIf(!input.SearchText.IsNullOrWhiteSpace(), x => x.Title.Contains(input.SearchText))
                .Select(x => new PostDto
                {
                    Content = x.Content.Length > 100 ? x.Content.Substring(0, 100) : x.Content,
                    Id = x.Id,
                    CreationTime = x.CreationTime,
                    Description = x.Description,
                    NumberOfComments = x.NumberOfComments,
                    NumberOfLikes = x.NumberOfLikes,
                    NumberOfViews = x.NumberOfViews,
                    PostUrl = x.PostUrl,
                    Title = x.Title
                });
            int totalCount = await queryPost.CountAsync();
            var posts = await queryPost.Skip((input.CurrentPage - 1) * input.PageSize).Take(input.PageSize).ToListAsync();
            return new PagedResultDto<PostDto>(totalCount, posts);
        }

        [RequestSizeLimit(1000_000_000)]
        public async Task<string> UploadImage([FromForm]IFormFile file)
        {
            string fileLocation = UploadFiles.CreateFolderIfNotExists(ConstantVariable.RootFolder, ConstantVariable.PostFolder);
            string fileName = await UploadFiles.UploadAsync(fileLocation, file);
            return $"{ConstantVariable.PostFolder}/{fileName}";
        }
    }
}
