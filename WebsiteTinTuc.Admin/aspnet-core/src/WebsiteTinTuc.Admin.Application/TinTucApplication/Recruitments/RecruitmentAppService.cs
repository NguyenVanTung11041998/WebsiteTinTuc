using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Constans;
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
    }
}
