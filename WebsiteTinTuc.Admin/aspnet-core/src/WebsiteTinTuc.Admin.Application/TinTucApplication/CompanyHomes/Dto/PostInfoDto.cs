using System;
using System.Collections.Generic;
using WebsiteTinTuc.Admin.Entities;

namespace WebsiteTinTuc.Admin.TinTucApplication.CompanyHomes.Dto
{
    public class PostInfoDto : CompanyHomeInfo
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public JobType JobType { get; set; }
        public ExperienceType ExperienceType { get; set; }
        public MoneyType MoneyType { get; set; }
        public long MinMoney { get; set; }
        public long MaxMoney { get; set; }
        public string LevelName { get; set; }
        public int? TimeExperience { get; set; }
        public DateTime? EndDate { get; set; }
        public string PostUrl { get; set; }
        public List<HashtagCompanyHome> HashtagPosts { get; set; }
    }
}
