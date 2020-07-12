using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTinTuc.Admin.Entities
{
    public class Asset : FullAuditedEntity<Guid>
    {
        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        public string Path { get; set; }
        public FileType FileType { get; set; }
        public virtual Post Post { get; set; }
    }

    public enum FileType
    {
        Thumbnail = 0,
        Image = 1
    }
}
