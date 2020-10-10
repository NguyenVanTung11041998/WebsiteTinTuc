using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.CompanyHomes.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.CompanyHomes
{
    public interface ICompanyHomeAppService
    {
        Task<CompanyHomeModel> GetCompanyByUrl(string url, PageRequest input);
        Task<PagedResultDto<PostCompanyHome>> GetPostOfCompanyPaging(string url, PageRequest input, Guid? postId = null);
        Task<List<CompanyJobHome>> GetAllBranchJobOfCompany(string url);
        Task<List<HashtagCompanyHome>> GetAllHashtagOfCompany(string url);
        Task<PostInfoDto> GetPostByUrl(string url, PageRequest input);
        Task<List<HashtagCompanyHome>> GetAllHashtagOfPost(string url);
    }
}
