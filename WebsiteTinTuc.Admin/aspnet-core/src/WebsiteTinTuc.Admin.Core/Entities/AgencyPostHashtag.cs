using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTinTuc.Admin.Entities
{
    public class CompanyPostHashtag : FullAuditedEntity<Guid>
    {
        [ForeignKey(nameof(Hashtag))]
        public Guid HashtagId { get; set; }
        [ForeignKey(nameof(Company))]
        public Guid? CompanyId { get; set; }
        [ForeignKey(nameof(Post))]
        public Guid? PostId { get; set; }
        public virtual Company Company { get; set; }
        public virtual Post Post { get; set; }
        public virtual Hashtag Hashtag { get; set; }
    }
}
