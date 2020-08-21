using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace WebsiteTinTuc.Admin.Entities
{
    public class BranchJob : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string BranchJobLocation { get; set; }
        public virtual ICollection<BranchJobAgency> BranchJobAgencies { get; set; }
    }
}
