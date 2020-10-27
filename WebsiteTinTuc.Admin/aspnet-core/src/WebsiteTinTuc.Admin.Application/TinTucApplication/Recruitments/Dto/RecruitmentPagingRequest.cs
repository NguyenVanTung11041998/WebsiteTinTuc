using System;
using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.TinTucApplication.Recruitments.Dto
{
    public class RecruitmentPagingRequest : PageRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid PostId { get; set; }
    }
}
