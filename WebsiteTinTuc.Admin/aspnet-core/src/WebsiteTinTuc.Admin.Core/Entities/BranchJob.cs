using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace WebsiteTinTuc.Admin.Entities
{
    public class BranchJob : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string BranchJobUrl { get; set; }
        public virtual ICollection<BranchJobCompany> BranchJobCompanies { get; set; }
    }
}
