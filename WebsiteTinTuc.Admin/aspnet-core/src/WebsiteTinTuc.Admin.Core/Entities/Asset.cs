using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTinTuc.Admin.Entities
{
    public class Asset : FullAuditedEntity<Guid>
    {
        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public string Path { get; set; }
        public FileType FileType { get; set; }
        public virtual Company Company { get; set; }
    }

    public enum FileType
    {
        Thumbnail = 0,
        Image = 1
    }
}
