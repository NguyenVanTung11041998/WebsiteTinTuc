using System;
using System.Collections.Generic;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.TinTucApplication.Posts.Dto
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
        public string PostUrl { get; set; }
        public long NumberOfViews { get; set; }
        public JobType JobType { get; set; }
        public long MinMoney { get; set; }
        public long MaxMoney { get; set; }
        public MoneyType MoneyType { get; set; }
        public int? TimeExperience { get; set; }
        public DateTime? EndDate { get; set; }
        public ExperienceType ExperienceType { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public Guid LevelId { get; set; }
        public string LevelName { get; set; }
        public List<HashtagCompanyPostModel> Hashtags { get; set; }
    }
}
