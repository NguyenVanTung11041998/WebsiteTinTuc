using System;
using System.Collections.Generic;
using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.TinTucApplication.CompanyHomes.Dto
{
    public class CompanyHomeModel : CompanyHomeInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FullNameCompany { get; set; }
        public ObjectFile Thumbnail { get; set; }
        public List<ObjectFile> Images { get; set; }
        public string Description { get; set; }
    }
}
