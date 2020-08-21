using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace WebsiteTinTuc.Admin.Entities
{
    public class Post : FullAuditedEntity<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string PostUrl { get; set; }
        public long NumberOfViews { get; set; }
        public JobType JobType { get; set; }
        public long MinMoney { get; set; }
        public long MaxMoney { get; set; }
        public MoneyType MoneyType { get; set; }
        public int? TimeExperience { get; set; }
        public DateTime? EndDate { get; set; }
        public ExperienceType ExperienceType { get; set; }
        public virtual ICollection<PostCategory> PostCategories { get; set; }
        public virtual ICollection<AgencyPostHashtag> AgencyHashtags { get; set; }
        public virtual ICollection<Level> Levels { get; set; }
        public virtual ICollection<CV> CVs { get; set; }
    }

    public enum JobType
    {
        PartTime = 0,
        FullTime = 1
    }

    public enum ExperienceType
    {
        NoExperience = 0,
        Month = 1,
        Year = 2
    }

    public enum MoneyType
    {
        NoNegotiable = 0,
        Negotiable = 1,
    }
}
