using Abp.Application.Services.Dto;
using System;

namespace WebsiteTinTuc.Admin.Users.Dto
{
    //custom PagedResultRequestDto
    public class PagedUserResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
