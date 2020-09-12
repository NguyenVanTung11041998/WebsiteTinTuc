using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace WebsiteTinTuc.Admin.Entities
{
    public class Hashtag : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string HashtagUrl { get; set; }
        public bool IsHot { get; set; }
        public virtual ICollection<CompanyPostHashtag> CompanyPostHashtags { get; set; }
    }
}
