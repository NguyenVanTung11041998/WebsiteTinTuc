using System;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.TinTucApplication.Homes.Dto
{
    public class CompanyPostModel
    {
        public Guid CompanyId { get; set; }
        public Guid PostId { get; set; }
        public string FullCompanyName { get; set; }
        public ObjectFile Thumbnail { get; set; }
        public string Location { get; set; }
        public int TimeCreateNewJob { get; set; }
        public long MinSalary { get; set; }
        public long MaxSalary { get; set; }
        public MoneyType MoneyType { get; set; }
        public string Title { get; set; }
        public string Treatment { get; set; }
        public string PostUrl { get; set; }
    }
}
