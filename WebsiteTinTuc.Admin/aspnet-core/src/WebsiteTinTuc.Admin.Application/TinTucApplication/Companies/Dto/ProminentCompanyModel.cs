using System;
using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.TinTucApplication.Companies.Dto
{
    public class ProminentCompanyModel
    {
        public ObjectFile Image { get; set; }
        public ObjectFile Thumbnail { get; set; }
        public string Name { get; set; }
        public string FullNameCompany { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public long NumberJobs { get; set; }
        public string CompanyUrl { get; set; }
        public Guid Id { get; set; }
    }
}
