using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebsiteTinTuc.Admin.Authorization.Users;

namespace WebsiteTinTuc.Admin.Entities
{
    public class Comment : FullAuditedEntity<Guid>
    {
        public string Content { get; set; }
        public long NumberOfLikes { get; set; }
        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
