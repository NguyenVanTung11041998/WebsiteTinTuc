using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.TinTucApplication.Companies.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Companies
{
    public interface ICompanyAppService
    {
        Task CreateCompanyAsync(CompanyRequest input);
        Task UpdateCompanyAsync(CompanyRequest input);
        Task DeleteCompanyAsync(Guid id);
        Task<PagedResultDto<CompanyModel>> GetCompanyPagingAsync(CompanyFilterPaging input);
        Task<CompanyModel> GetCompanyByIdAsync(Guid id);
        Task<List<CompanySelectModel>> GetAllCompanies();
        Task SettingHotOfCompany(Guid id);
        Task<List<ProminentCompanyModel>> GetTopCompanyProminent(int? count = null);
    }
}
