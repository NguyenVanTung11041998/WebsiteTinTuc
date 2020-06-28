using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace WebsiteTinTuc.Admin.Entities
{
    public class Post : FullAuditedEntity<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string PostUrl { get; set; }
        public long NumberOfViews { get; set; }
        public long NumberOfLikes { get; set; }
        public long NumberOfComments { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostCategory> PostCategories { get; set; }
        public virtual ICollection<PostHashtag> PostHashtags { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
    }
}
