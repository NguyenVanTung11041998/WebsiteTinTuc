using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.Nationalities.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Nationalities
{
    public interface INationalityAppService
    {
        Task<PagedResultDto<NationalityDto>> GetAllNationalityPagingAsync(PageRequest input);
        Task<List<NationalityDto>> GetAll();
        Task CreateNationalityAsync(NationalityRequest input);
        Task UpdateNationality(NationalityRequest input);
        Task DeleteNationality(Guid id);
    }
}
