using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace WebsiteTinTuc.Admin.Entities
{
    public class Level : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
