using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Authorization;
using WebsiteTinTuc.Admin.Constans;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Helpers;
using WebsiteTinTuc.Admin.Models;
using WebsiteTinTuc.Admin.TinTucApplication.Nationalities.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Nationalities
{
    [AbpAuthorize]
    public class NationalityAppService : AdminAppServiceBase, INationalityAppService
    {
        [AbpAuthorize(PermissionNames.Pages_Create_Nationality)]
        public async Task CreateNationalityAsync([FromForm]NationalityRequest input)
        {
            bool checkName = await WorkScope.GetAll<Nationality>().AnyAsync(x => x.Name == input.Name);
            if (checkName)
                throw new UserFriendlyException("Quốc tịch đã tồn tại");

            string fileName = string.Empty;
            if (input.Image?.Length > 0)
            {
                string fileLocation = UploadFiles.CreateFolderIfNotExists(ConstantVariable.RootFolder, $@"{ConstantVariable.UploadFolder}\{ConstantVariable.Company}");
                fileName = await UploadFiles.UploadAsync(fileLocation, input.Image);
            }

            var nationality = new Nationality
            {
                Name = input.Name,
                Path = fileName
            };

            await WorkScope.InsertAsync(nationality);
        }

        [AbpAuthorize(PermissionNames.Pages_Delete_Nationality)]
        public async Task DeleteNationality(Guid id)
        {
            var nationality = await WorkScope.GetAll<Nationality>().FirstOrDefaultAsync(x => x.Id == id);
            if (nationality == default)
                throw new UserFriendlyException("Quốc tịch không tồn tại");

            await WorkScope.DeleteAsync(nationality);
        }

        [AbpAuthorize(PermissionNames.Pages_View_Nationality)]
        public async Task<PagedResultDto<NationalityDto>> GetAllNationalityPagingAsync(PageRequest input)
        {
            var query = WorkScope.GetAll<Nationality>()
                                       .WhereIf(!input.SearchText.IsNullOrWhiteSpace(), x => x.Name.Contains(input.SearchText))
                                       .Select(x => new NationalityDto
                                       {
                                           Image = new ObjectFile { Path = x.Path, FileType = FileType.Image },
                                           Id = x.Id,
                                           Name = x.Name,
                                           CreationTime = x.CreationTime
                                       });
            int totalCount = await query.CountAsync();
            var nationalities = await query.PageBy((input.CurrentPage - 1) * input.PageSize, input.PageSize).ToListAsync();
            return new PagedResultDto<NationalityDto>(totalCount, nationalities);
        }

        [AbpAuthorize(PermissionNames.Pages_Update_Nationality)]
        public async Task UpdateNationality([FromForm]NationalityRequest input)
        {
            bool checkName = await WorkScope.GetAll<Nationality>().AnyAsync(x => x.Name == input.Name && x.Id != input.Id);
            if (checkName)
                throw new UserFriendlyException("Quốc tịch đã tồn tại");

            var nationality = await WorkScope.GetAll<Nationality>().FirstOrDefaultAsync(x => x.Id == input.Id);
            if (nationality == default)
                throw new UserFriendlyException("Quốc tịch không tồn tại");

            nationality.Name = input.Name;

            if (input.Image?.Length > 0)
            {
                string fileLocation = UploadFiles.CreateFolderIfNotExists(ConstantVariable.RootFolder, $@"{ConstantVariable.UploadFolder}\{ConstantVariable.Company}");
                nationality.Path = await UploadFiles.UploadAsync(fileLocation, input.Image);
            }

            await WorkScope.UpdateAsync(nationality);
        }
    }
}
