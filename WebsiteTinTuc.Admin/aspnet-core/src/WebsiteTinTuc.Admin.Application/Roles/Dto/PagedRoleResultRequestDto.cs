using Abp.Application.Services.Dto;

namespace WebsiteTinTuc.Admin.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

