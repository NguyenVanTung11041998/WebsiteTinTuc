using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTinTuc.Admin.Entities
{
    public class Level : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
