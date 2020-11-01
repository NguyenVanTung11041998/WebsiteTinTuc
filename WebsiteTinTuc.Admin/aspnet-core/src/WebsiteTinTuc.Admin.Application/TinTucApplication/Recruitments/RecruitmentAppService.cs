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
using WebsiteTinTuc.Admin.Authorization.Users;
using WebsiteTinTuc.Admin.Constants;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Helpers;
using WebsiteTinTuc.Admin.TinTucApplication.Recruitments.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Recruitments
{
    public class RecruitmentAppService : AdminAppServiceBase, IRecruitmentAppService
    {
        [RequestSizeLimit(1_000_000_000)]
        public async Task CreateCVAsync([FromForm] RecruitmentRequest input)
        {
            bool checkExist = await WorkScope.GetAll<CV>().AnyAsync(x => x.PostId == input.PostId
                                                && x.UserId == input.UserId
                                                && input.UserId.HasValue);

            if (checkExist) throw new UserFriendlyException("Bạn đã ứng tuyển vị trí này");

            var post = await WorkScope.GetAsync<Post>(input.PostId);

            var cv = ObjectMapper.Map<CV>(input);

            if (input.FileCV?.Length > 0)
            {
                string fileLocation = UploadFiles.CreateFolderIfNotExists(ConstantVariable.RootFolder, $@"{ConstantVariable.UploadFolder}\{ConstantVariable.CV}\{post.PostUrl}");
                string fileName = await UploadFiles.UploadAsync(fileLocation, input.FileCV);
                cv.Link = $"{ConstantVariable.UploadFolder}/{ConstantVariable.CV}/{post.PostUrl}/{fileName}";
            }

            await WorkScope.InsertAsync(cv);
        }

        [AbpAuthorize(PermissionNames.Pages_View_CV)]
        public async Task<PagedResultDto<RecruitmentDto>> GetAllRecruitmentPagingAsync(RecruitmentPagingRequest input)
        {
            var user = await GetCurrentUserAsync();

            var query = WorkScope.GetAll<CV>()
                .Where(x => x.PostId == input.PostId)
                .Include(x => x.Post)
                .WhereIf(user.UserType == UserType.Hr, x => x.Post.CreatorUserId == user.Id)
                .WhereIf(!input.SearchText.IsNullOrWhiteSpace(), x => x.Name.Contains(input.SearchText))
                .WhereIf(input.StartDate.HasValue, x => x.CreationTime >= input.StartDate)
                .WhereIf(input.EndDate.HasValue, x => x.CreationTime < input.EndDate)
                .OrderByDescending(x => x.CreationTime)
                .Select(x => new RecruitmentDto
                {
                    Id = x.Id,
                    CreationTime = x.CreationTime,
                    Email = x.Email,
                    Link = x.Link,
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    Portfolio = x.Portfolio,
                    UserName = x.UserName,
                    UserId = x.UserId,
                    IsRead = x.IsRead,
                    PostId = x.PostId
                });

            int totalCount = await query.CountAsync();

            var list = await query.PageBy((input.CurrentPage - 1) * input.PageSize, input.PageSize).ToListAsync();

            return new PagedResultDto<RecruitmentDto>(totalCount, list);
        }

        [AbpAuthorize(PermissionNames.Pages_View_CV)]
        public async Task ReadCVAsync(Guid id)
        {
            var cv = await WorkScope.GetAsync<CV>(id);
            cv.IsRead = true;
            await WorkScope.UpdateAsync(cv);
        }
    }
}
