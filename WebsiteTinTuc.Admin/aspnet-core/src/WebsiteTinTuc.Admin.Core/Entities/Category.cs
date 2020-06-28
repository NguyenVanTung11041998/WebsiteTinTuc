using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace WebsiteTinTuc.Admin.Entities
{
    public class Category : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string CategoryUrl { get; set; }
        public virtual ICollection<PostCategory> PostCategories { get; set; }
    }
}
