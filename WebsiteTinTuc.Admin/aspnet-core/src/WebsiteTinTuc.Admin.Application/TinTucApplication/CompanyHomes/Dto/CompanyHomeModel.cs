using System;
using System.Collections.Generic;
using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.TinTucApplication.CompanyHomes.Dto
{
    public class CompanyHomeModel : CompanyHomeInfo
    {
        public Guid Id { get; set; }
        public List<ObjectFile> Images { get; set; }
        public string Description { get; set; }
    }
}
