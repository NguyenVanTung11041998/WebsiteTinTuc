using Abp.Domain.Entities.Auditing;
using System;

namespace WebsiteTinTuc.Admin.Entities
{
    public class Asset : FullAuditedEntity<Guid>
    {
        public Guid PostId { get; set; }
        public string Path { get; set; }
        public FileType FileType { get; set; }
    }

    public enum FileType
    {
        Thumbnail = 0,
        Image = 1
    }
}
