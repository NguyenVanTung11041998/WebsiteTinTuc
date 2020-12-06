using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Authorization;
using WebsiteTinTuc.Admin.Authorization.Users;
using WebsiteTinTuc.Admin.Constants;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Helpers;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.Net.MimeTypes;
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

            foreach (var item in input.HashtagIds)
            {
                await WorkScope.InsertAsync(new CompanyPostHashtag
                {
                    HashtagId = item,
                    PostId = id
                });
            }

            var request = new PostSitemap
            {
                Id = id,
                AgencyName = company.Name,
                IsCreate = true,
                PostUrl = post.PostUrl,
                Title = post.Title
            };
            await SaveSiteMap(request);
        }

        public async Task DeletePostAsync(Guid id)
        {
            var user = await GetCurrentUserAsync();
            var post = await WorkScope.GetAll<Post>()
                .WhereIf(user.UserType == UserType.Hr, x => x.CreatorUserId == user.Id)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (post == default)
                throw new UserFriendlyException("Bài viết không thể xóa");

            await WorkScope.DeleteAsync(post);
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.Pages_Update_Post)]
        public async Task EditPostAsync(PostRequest input)
        {
            var post = await WorkScope.GetAll<Post>().FirstOrDefaultAsync(x => x.Id == input.Id);
            if (post == default)
                throw new UserFriendlyException("Không tồn tại bài viết");

            ObjectMapper.Map(input, post);
            var company = await WorkScope.GetAsync<Company>(input.CompanyId);
            int numberPostSameTitle = await WorkScope.GetAll<Post>()
                    .Where(x => x.Title == input.Title && x.CompanyId == input.CompanyId && x.Id != input.Id)
                    .CountAsync();

            post.PostUrl = numberPostSameTitle > 0 
                ? $"{input.Title.RemoveSign4VietnameseString().ToIdentifier()}-{company.CompanyUrl}-{numberPostSameTitle + 1}"
                : $"{input.Title.RemoveSign4VietnameseString().ToIdentifier()}-{company.CompanyUrl}";
            await WorkScope.UpdateAsync(post);

            foreach (var item in input.HashtagIds)
            {
                await WorkScope.InsertAsync(new CompanyPostHashtag
                {
                    HashtagId = item,
                    PostId = post.Id
                });
            }

            foreach (var item in input.HashtagIdDeletes)
            {
                await WorkScope.DeleteAsync<CompanyPostHashtag>(item);
            }

            var request = new PostSitemap
            {
                Id = post.Id,
                AgencyName = company.Name,
                IsCreate = false,
                PostUrl = post.PostUrl,
                Title = post.Title
            };
            await SaveSiteMap(request);
        }

        [AbpAuthorize(PermissionNames.Pages_View_Post)]
        public async Task<PagedResultDto<PostDto>> GetAllPostPagingAsync(PostPagingRequest input)
        {
            var user = await GetCurrentUserAsync();

            var query = WorkScope.GetAll<Post>()
                                 .WhereIf(!input.SearchText.IsNullOrWhiteSpace(), x => x.Title.Contains(input.SearchText))
                                 .WhereIf(user.UserType == UserType.Hr, x => x.CreatorUserId == user.Id)
                                 .WhereIf(input.JobType.HasValue, x => x.JobType == input.JobType)
                                 .Include(x => x.Company)
                                 .WhereIf(!input.CompanyName.IsNullOrWhiteSpace(), x => x.Company.FullNameCompany.Contains(input.CompanyName))
                                 .Include(x => x.CompanyPostHashtags)
                                 .WhereIf(input.HashtagId.HasValue, x => x.CompanyPostHashtags.Any(p => p.HashtagId == input.HashtagId))
                                 .WhereIf(input.StartDate.HasValue, x => x.CreationTime >= input.StartDate)
                                 .WhereIf(input.EndDate.HasValue, x => x.EndDate < input.EndDate)
                                 .OrderByDescending(x => x.CreationTime);

            int totalCount = await query.CountAsync();
            var list = await query.PageBy((input.CurrentPage - 1) * input.PageSize, input.PageSize)
                .Select(x => new PostDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    CreationTime = x.CreationTime,
                    NumberOfViews = x.NumberOfViews,
                    CompanyName = x.Company.Name,
                    JobType = x.JobType,
                    EndDate = x.EndDate
                }).ToListAsync();
            return new PagedResultDto<PostDto>(totalCount, list);
        }

        [AbpAuthorize(PermissionNames.Pages_View_Post)]
        public async Task<PostDto> GetPostByIdAsync(Guid id)
        {
            var user = await GetCurrentUserAsync();
            var post = await WorkScope.GetAll<Post>()
                .WhereIf(user.UserType == UserType.Hr, x => x.CreatorUserId == user.Id)
                .Include(x => x.CompanyPostHashtags)
                .Select(x => new PostDto
                {
                    CompanyId = x.CompanyId,
                    Content = x.Content,
                    EndDate = x.EndDate,
                    ExperienceType = x.ExperienceType,
                    Id = x.Id,
                    JobType = x.JobType,
                    LevelId = x.LevelId,
                    MaxMoney = x.MaxMoney,
                    MinMoney = x.MinMoney,
                    MoneyType = x.MoneyType,
                    NumberOfViews = x.NumberOfViews,
                    TimeExperience = x.TimeExperience,
                    Title = x.Title,
                    Hashtags = x.CompanyPostHashtags.Select(p => new HashtagCompanyPostModel
                    {
                        Id = p.Id,
                        HashtagId = p.HashtagId,
                        PostId = x.Id
                    }).ToList()
                }).FirstOrDefaultAsync(x => x.Id == id);

            return post;
        }

        [AbpAllowAnonymous]
        public async Task<List<PostTopProminent>> GetTopPostProminent(int? count = null)
        {
            var query = WorkScope.GetAll<Post>()
                        .Include(x => x.Company)
                        .Where(x => x.Company.IsHot)
                        .Select(x => new PostTopProminent
                        {
                            CompanyName = x.Company.Name,
                            Id = x.Id,
                            PostUrl = x.PostUrl,
                            Title = x.Title
                        });

            if (count.HasValue)
                query = query.Take(count.Value);

            var list = await query.ToListAsync();
            return list;
        }

        private async Task SaveSiteMap(PostSitemap input)
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConstantVariable.WebUserUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MimeTypeNames.ApplicationJson));
            var response = await httpClient.PostAsJsonAsync("api/SiteMap", input);
            if (!response.IsSuccessStatusCode)
                throw new UserFriendlyException("Update sitemap fail, check web user");
        }
    }
}
