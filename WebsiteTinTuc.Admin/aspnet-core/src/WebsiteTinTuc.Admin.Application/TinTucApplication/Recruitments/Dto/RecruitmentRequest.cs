using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using WebsiteTinTuc.Admin.Entities;

namespace WebsiteTinTuc.Admin.TinTucApplication.Recruitments.Dto
{
    [AutoMap(typeof(CV))]
    public class RecruitmentRequest
    {
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Portfolio { get; set; }
        public Guid PostId { get; set; }
        public IFormFile FileCV { get; set; }
    }
}
