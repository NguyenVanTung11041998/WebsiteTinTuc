using System.Threading.Tasks;
using WebsiteTinTuc.Admin.TinTucApplication.Recruitments.Dto;

namespace WebsiteTinTuc.Admin.TinTucApplication.Recruitments
{
    public interface IRecruitmentAppService
    {
        Task CreateCVAsync(RecruitmentRequest input);
    }
}
