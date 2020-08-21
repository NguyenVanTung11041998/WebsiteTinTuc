using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTinTuc.Admin.Entities
{
    public class BranchJobAgency : FullAuditedEntity<Guid>
    {
        [ForeignKey(nameof(Agency))]
        public Guid AgencyId { get; set; }
        [ForeignKey(nameof(BranchJob))]
        public Guid BranchJobId { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual BranchJob BranchJob { get; set; }
    }
}
