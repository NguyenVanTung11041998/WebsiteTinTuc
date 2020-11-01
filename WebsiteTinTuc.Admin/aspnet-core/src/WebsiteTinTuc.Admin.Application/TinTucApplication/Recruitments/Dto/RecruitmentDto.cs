using Abp.Application.Services.Dto;
using System;

namespace WebsiteTinTuc.Admin.TinTucApplication.Recruitments.Dto
{
    public class RecruitmentDto : EntityDto<Guid>
    {
        public long? UserId { get; set; }
        public string Link { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Portfolio { get; set; }
        public bool IsRead { get; set; }
        public Guid PostId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
