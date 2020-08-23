using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace WebsiteTinTuc.Admin.Entities
{
    public class Nationality : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}
