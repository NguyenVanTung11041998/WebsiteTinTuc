using Abp.Application.Services.Dto;
using System.Collections.Generic;
using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.TinTucApplication.CompanyHomes.Dto
{
    public class CompanyHomeInfo
    {
        public string Name { get; set; }
        public string FullNameCompany { get; set; }
        public ObjectFile Thumbnail { get; set; }
        public string Website { get; set; }
        public string LocationDescription { get; set; }
        public string Location { get; set; }
        public string Treatment { get; set; }
        public int? MinScale { get; set; }
        public string CompanyUrl { get; set; }
        public int? MaxScale { get; set; }
        public List<CompanyJobHome> CompanyJobs { get; set; }
        public List<HashtagCompanyHome> Hashtags { get; set; }
        public NationalityCompanyDto Nationality { get; set; }
        public PagedResultDto<PostCompanyHome> Posts { get; set; }
    }
}
