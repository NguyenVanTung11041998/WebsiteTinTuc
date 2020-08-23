using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTinTuc.Admin.Entities
{
    public class BranchJobCompany : FullAuditedEntity<Guid>
    {
        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        [ForeignKey(nameof(BranchJob))]
        public Guid BranchJobId { get; set; }
        public virtual Company Company { get; set; }
        public virtual BranchJob BranchJob { get; set; }
    }
}
