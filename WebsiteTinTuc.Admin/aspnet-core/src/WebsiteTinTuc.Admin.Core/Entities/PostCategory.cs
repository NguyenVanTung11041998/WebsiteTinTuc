using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTinTuc.Admin.Entities
{
    public class PostCategory : FullAuditedEntity<Guid>
    {
        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
        public virtual Category Category { get; set; }
    }
}
