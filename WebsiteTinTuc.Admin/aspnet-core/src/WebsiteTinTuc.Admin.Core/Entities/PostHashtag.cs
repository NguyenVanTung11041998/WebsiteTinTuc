using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTinTuc.Admin.Entities
{
    public class PostHashtag : FullAuditedEntity<Guid>
    {
        [ForeignKey(nameof(Hashtag))]
        public Guid HashtagId { get; set; }
        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
        public virtual Hashtag Hashtag { get; set; }
    }
}
