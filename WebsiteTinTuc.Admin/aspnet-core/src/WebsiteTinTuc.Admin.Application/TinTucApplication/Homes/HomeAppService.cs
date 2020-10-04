using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.Homes.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Homes
{
    public class HomeAppService : AdminAppServiceBase, IHomeAppService
    {
        public async Task<PagedResultDto<CompanyPostModel>> GetAllCompanyPostPaging(HomeFilter input)
        {
            var companyQuery = WorkScope.GetAll<Company>()
                                        .Include(x => x.Posts)
                                        .Include(x => x.Assets)
                                        .WhereIf(input.IsHot.HasValue, x => x.IsHot == input.IsHot)
                                        .Where(x => x.Posts.Any())
                                        .OrderByDescending(x => x.CreationTime)
                                        .PageBy((input.CurrentPage - 1) * input.PageSize, input.PageSize)
                                        .Select(x => new
                                        {
                                            Company = x,
                                            Thumbnail = x.Assets.Where(p => p.FileType == FileType.Thumbnail)
                                                .Select(p => new ObjectFile
                                                {
                                                    Id = p.Id,
                                                    FileType = p.FileType,
                                                    Path = p.Path
                                                }).FirstOrDefault(),
                                            Post = x.Posts.OrderByDescending(p => p.CreationTime)
                                                        .FirstOrDefault()
                                        });

            int totalCount = await companyQuery.CountAsync();
            DateTime localTime = GetLocalTime();

            var result = await companyQuery.Select(x => new CompanyPostModel
            {
                FullCompanyName = x.Company.FullNameCompany,
                Location = x.Company.Location,
                CompanyId = x.Company.Id,
                MaxSalary = x.Post.MaxMoney,
                MinSalary = x.Post.MinMoney,
                MoneyType = x.Post.MoneyType,
                PostId = x.Post.Id,
                PostUrl = x.Post.PostUrl,
                Title = x.Post.Title,
                Treatment = x.Company.Treatment,
                Thumbnail = x.Thumbnail,
                TimeCreateNewJob = (int)(localTime - x.Post.CreationTime).TotalHours
            }).ToListAsync();

            return new PagedResultDto<CompanyPostModel>(totalCount, result);
        }

        public async Task<List<CompanyPostModel>> GetTopNewPost(int count = 10)
        {
            var queryHashtags = WorkScope.GetAll<Hashtag>();
            var queryBranchJobs = WorkScope.GetAll<BranchJob>();
            DateTime localTime = GetLocalTime();
            var posts = await WorkScope.GetAll<Post>()
                                .OrderByDescending(x => x.CreationTime)
                                .Take(count)
                                .Include(x => x.Company)
                                .Include(x => x.Company.Assets)
                                .Include(x => x.Company.BranchJobCompanies)
                                .Include(x => x.CompanyPostHashtags)
                                .Select(x => new CompanyPostModel
                                {
                                    FullCompanyName = x.Company.FullNameCompany,
                                    CompanyName = x.Company.Name,
                                    Location = x.Company.Location,
                                    CompanyId = x.Company.Id,
                                    MaxSalary = x.MaxMoney,
                                    MinSalary = x.MinMoney,
                                    MoneyType = x.MoneyType,
                                    PostId = x.Id,
                                    PostUrl = x.PostUrl,
                                    Title = x.Title,
                                    Treatment = x.Company.Treatment,
                                    Thumbnail = x.Company.Assets.Where(p => p.FileType == FileType.Thumbnail)
                                                .Select(p => new ObjectFile
                                                {
                                                    Id = p.Id,
                                                    FileType = p.FileType,
                                                    Path = p.Path
                                                }).FirstOrDefault(),
                                    TimeCreateNewJob = (int)(localTime - x.CreationTime).TotalHours,
                                    PostHashtags = queryHashtags.Where(p => x.CompanyPostHashtags.Any(k => k.HashtagId == p.Id))
                                                        .Select(p => new HashtagHomeModel
                                                        {
                                                            HashtagId = p.Id,
                                                            Name = p.Name,
                                                            HashtagUrl = p.HashtagUrl
                                                        }).ToList(),
                                    CompanyJobs = queryBranchJobs.Where(p => x.Company.BranchJobCompanies.Any(k => k.BranchJobId == p.Id))
                                                        .Select(p => new BranchJobCompanyHome
                                                        {
                                                            BranchJobId = p.Id,
                                                            Name = p.Name,
                                                            BranchJobUrl = p.BranchJobUrl
                                                        }).ToList()
                                }).ToListAsync();

            return posts;
        }
    }
}
