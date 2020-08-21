using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace WebsiteTinTuc.Admin.Entities
{
    public class Hashtag : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string HashtagUrl { get; set; }
        public HashtagType HashtagType { get; set; }
        public virtual ICollection<AgencyPostHashtag> AgencyPostHashtags { get; set; }
    }

    public enum HashtagType
    {
        Agency = 0,
        Post = 1
    }
}
