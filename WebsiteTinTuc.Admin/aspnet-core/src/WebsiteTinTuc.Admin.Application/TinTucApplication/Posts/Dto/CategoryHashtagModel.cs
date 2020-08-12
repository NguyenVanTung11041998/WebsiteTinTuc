using System;

namespace WebsiteTinTuc.Admin.TinTucApplication.Posts.Dto
{
    public class CategoryHashtagModel
    {
        public Guid Id { get; set; }
        public Guid CategoryHashtagOfPostId { get; set; }
    }
}
