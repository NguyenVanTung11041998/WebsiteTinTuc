using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTinTuc.Admin.Entities
{
    public class AgencyPostHashtag : FullAuditedEntity<Guid>
    {
        [ForeignKey(nameof(Hashtag))]
        public Guid HashtagId { get; set; }
        [ForeignKey(nameof(Agency))]
        public Guid? AgencyId { get; set; }
        [ForeignKey(nameof(Post))]
        public Guid? PostId { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual Post Post { get; set; }
        public virtual Hashtag Hashtag { get; set; }
    }
}
