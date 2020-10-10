using System;
using System.Collections.Generic;
using WebsiteTinTuc.Admin.Entities;

namespace WebsiteTinTuc.Admin.TinTucApplication.CompanyHomes.Dto
{
    public class PostCompanyHome
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public long MinMoney { get; set; }
        public long MaxMoney { get; set; }
        public MoneyType MoneyType { get; set; }
        public string PostUrl { get; set; }
        public int TimeCreateNewJob { get; set; }
        public List<HashtagCompanyHome> Hashtags { get; set; }
    }
}
