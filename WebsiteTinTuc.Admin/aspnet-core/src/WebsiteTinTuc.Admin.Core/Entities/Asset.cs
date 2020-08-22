using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTinTuc.Admin.Entities
{
    public class Asset : FullAuditedEntity<Guid>
    {
        [ForeignKey(nameof(Agency))]
        public Guid AgencyId { get; set; }
        public string Path { get; set; }
        public FileType FileType { get; set; }
        public virtual Agency Agency { get; set; }
    }

    public enum FileType
    {
        Thumbnail = 0,
        Image = 1,
        NationalityImage = 2
    }
}
