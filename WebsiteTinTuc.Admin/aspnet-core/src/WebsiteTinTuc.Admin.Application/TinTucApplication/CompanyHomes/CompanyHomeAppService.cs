using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.CompanyHomes.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.CompanyHomes
{
    public class CompanyHomeAppService : AdminAppServiceBase, ICompanyHomeAppService
    {
        public async Task<CompanyHomeModel> GetCompanyByUrl(string url, PageRequest input)
        {
            var company = await WorkScope.GetAll<Company>()
                            .Include(x => x.Assets)
                            .Include(x => x.BranchJobCompanies)
                            .Include(x => x.Nationality)
                            .Include(x => x.CompanyPostHashtags)
                            .Select(x => new CompanyHomeModel
                            {
                                Id = x.Id,
                                CompanyUrl = x.CompanyUrl,
                                Description = x.Description,
                                FullNameCompany = x.FullNameCompany,
                                Name = x.Name,
                                LocationDescription = x.LocationDescription,
                                Website = x.Website,
                                Treatment = x.Treatment,
                                Location = x.Location,
                                MinScale = x.MinScale,
                                MaxScale = x.MaxScale,
                                Nationality = new NationalityCompanyDto
                                {
                                    Name = x.Nationality.Name,
                                    Path = x.Nationality.Path
                                },
                                Thumbnail = x.Assets.Where(p => p.FileType == FileType.Thumbnail)
                                                .Select(p => new ObjectFile
                                                {
                                                    Id = p.Id,
                                                    FileType = p.FileType,
                                                    Path = p.Path
                                                }).FirstOrDefault(),
                                Images = x.Assets.Where(p => p.FileType == FileType.Image)
                                                .Select(p => new ObjectFile
                                                {
                                                    Id = p.Id,
                                                    FileType = p.FileType,
                                                    Path = p.Path
                                                }).ToList()
                            })
                            .FirstOrDefaultAsync(x => x.CompanyUrl == url);

            if (company == null) return null;

            company.Posts = await GetPostOfCompanyPaging(url, input);
            company.CompanyJobs = await GetAllBranchJobOfCompany(url);
            company.Hashtags = await GetAllHashtagOfCompany(url);
            return company;
        }

        public async Task<PagedResultDto<PostCompanyHome>> GetPostOfCompanyPaging(string url, PageRequest input, Guid? postId = null)
        {
            var company = await WorkScope.GetAll<Company>().Where(x => x.CompanyUrl == url)
                                                .Select(x => new
                                                {
                                                    x.Id,
                                                    x.Location
                                                }).FirstOrDefaultAsync();

            var queryHashtags = WorkScope.GetAll<CompanyPostHashtag>().Include(x => x.Hashtag);

            DateTime now = GetLocalTime();

            var queryPost = WorkScope.GetAll<Post>().Where(x => x.CompanyId == company.Id)
                            .WhereIf(postId.HasValue, x => x.Id != postId);
            int totalCount = await queryPost.CountAsync();
            var posts = await queryPost.PageBy((input.CurrentPage - 1) * input.PageSize, input.PageSize)
                .Select(x => new PostCompanyHome
                {
                    Id = x.Id,
                    Location = company.Location,
                    MaxMoney = x.MaxMoney,
                    MinMoney = x.MinMoney,
                    MoneyType = x.MoneyType,
                    Title = x.Title,
                    TimeCreateNewJob = (int)(now - x.CreationTime).TotalHours,
                    PostUrl = x.PostUrl,
                    Hashtags = queryHashtags.Where(p => p.PostId == x.Id)
                                                        .OrderByDescending(x => x.CreationTime)
                                                        .Select(p => new HashtagCompanyHome
                                                        {
                                                            Id = p.Id,
                                                            Name = p.Hashtag.Name,
                                                            HashtagUrl = p.Hashtag.HashtagUrl
                                                        }).ToList()
                }).ToListAsync();
            return new PagedResultDto<PostCompanyHome>(totalCount, posts);
        }

        public async Task<List<CompanyJobHome>> GetAllBranchJobOfCompany(string url)
        {
            var brachJobs = await WorkScope.GetAll<Company>().Where(x => x.CompanyUrl == url)
                                                .Include(x => x.BranchJobCompanies)
                                                .Select(x => x.BranchJobCompanies.ToList())
                                                .FirstOrDefaultAsync();
            var branchJobQuery = WorkScope.GetAll<BranchJob>();
            var result = from x in brachJobs
                         join p in branchJobQuery on x.BranchJobId equals p.Id
                         select new CompanyJobHome
                         {
                             Id = p.Id,
                             CompanyJobUrl = p.BranchJobUrl,
                             Name = p.Name
                         };
            return result.ToList();
        }

        public async Task<List<HashtagCompanyHome>> GetAllHashtagOfCompany(string url)
        {
            var hashtags = await WorkScope.GetAll<Company>().Where(x => x.CompanyUrl == url)
                                                .Include(x => x.CompanyPostHashtags)
                                                .Select(x => x.CompanyPostHashtags.ToList())
                                                .FirstOrDefaultAsync();
            var hashtagQuery = WorkScope.GetAll<Hashtag>();
            var result = from x in hashtags
                         join p in hashtagQuery on x.HashtagId equals p.Id
                         select new HashtagCompanyHome
                         {
                             Id = p.Id,
                             HashtagUrl = p.HashtagUrl,
                             Name = p.Name
                         };
            return result.ToList();
        }

        public async Task<List<HashtagCompanyHome>> GetAllHashtagOfPost(string url)
        {
            var hashtags = await WorkScope.GetAll<Post>().Where(x => x.PostUrl == url)
                                                .Include(x => x.CompanyPostHashtags)
                                                .Select(x => x.CompanyPostHashtags.ToList())
                                                .FirstOrDefaultAsync();

            var hashtagQuery = WorkScope.GetAll<Hashtag>();
            var result = from x in hashtags
                         join p in hashtagQuery on x.HashtagId equals p.Id
                         select new HashtagCompanyHome
                         {
                             Id = p.Id,
                             HashtagUrl = p.HashtagUrl,
                             Name = p.Name
                         };
            return result.ToList();
        }

        public async Task<PostInfoDto> GetPostByUrl(string url, PageRequest input)
        {
            var post = await WorkScope.GetAll<Post>()
                            .Where(x => x.PostUrl == url)
                            .Include(x => x.Company)
                            .Include(x => x.Company.Nationality)
                            .Include(x => x.Level)
                            .Select(x => new PostInfoDto
                            {
                                Id = x.Id,
                                Title = x.Title,
                                CompanyUrl = x.Company.CompanyUrl,
                                Content = x.Content,
                                EndDate = x.EndDate,
                                ExperienceType = x.ExperienceType,
                                JobType = x.JobType,
                                LevelName = x.Level.Name,
                                Location = x.Company.Location,
                                MaxMoney = x.MaxMoney,
                                MinMoney = x.MinMoney,
                                MaxScale = x.Company.MaxScale,
                                MinScale = x.Company.MinScale,
                                MoneyType = x.MoneyType,
                                Nationality = new NationalityCompanyDto
                                {
                                    Name = x.Company.Nationality.Name,
                                    Path = x.Company.Nationality.Path
                                },
                                LocationDescription = x.Company.LocationDescription,
                                PostUrl = x.PostUrl,
                                TimeExperience = x.TimeExperience,
                                Website = x.Company.Website,
                                Treatment = x.Company.Treatment
                            }).FirstOrDefaultAsync();
            post.CompanyJobs = await GetAllBranchJobOfCompany(post.CompanyUrl);
            post.Hashtags = await GetAllHashtagOfCompany(post.CompanyUrl);
            post.Posts = await GetPostOfCompanyPaging(post.CompanyUrl, input, post.Id);
            post.HashtagPosts = await GetAllHashtagOfPost(post.PostUrl);
            return post;
        }
    }
}
