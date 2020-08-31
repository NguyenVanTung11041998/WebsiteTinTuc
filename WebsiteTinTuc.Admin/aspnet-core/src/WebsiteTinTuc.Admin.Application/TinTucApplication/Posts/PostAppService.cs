using Abp.Application.Services.Dto;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Authorization;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Helpers;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.Posts.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Posts
{
    [AbpAuthorize]
    public class PostAppService : AdminAppServiceBase, IPostAppService
    {
        [AbpAuthorize(PermissionNames.Pages_Create_Post)]
        public async Task CreatePostAsync(PostRequest input)
        {
            var post = ObjectMapper.Map<Post>(input);
            var company = await WorkScope.GetAsync<Company>(input.CompanyId);
            int numberPostSameTitle = await WorkScope.GetAll<Post>()
                    .Where(x => x.Title == input.Title && x.CompanyId == input.CompanyId)
                    .CountAsync() + 1;

            post.PostUrl = $"{input.Title.RemoveSign4VietnameseString().ToIdentifier()}-{company.CompanyUrl}-{numberPostSameTitle}";
            Guid id = await WorkScope.InsertAndGetIdAsync(post);

        }

        public async Task EditPostAsync(PostRequest input)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResultDto<PostDto>> GetAllPostPagingAsync(PageRequest input)
        {
            throw new NotImplementedException();
        }

        public async Task<PostDto> GetPostByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
