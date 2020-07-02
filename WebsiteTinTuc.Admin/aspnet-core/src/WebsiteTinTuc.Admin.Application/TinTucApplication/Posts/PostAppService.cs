using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Constans;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Helpers;
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
    }
}
