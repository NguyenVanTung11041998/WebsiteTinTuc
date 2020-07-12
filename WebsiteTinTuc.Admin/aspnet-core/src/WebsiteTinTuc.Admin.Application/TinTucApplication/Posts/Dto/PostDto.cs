using System;
using System.Collections.Generic;
using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.TinTucApplication.Posts.Dto
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public ObjectFile ObjectFile { get; set; }
        public long NumberOfViews { get; set; }
        public long NumberOfComments { get; set; }
        public long NumberOfLikes { get; set; }
        public string PostUrl { get; set; }
        public IEnumerable<CategoryHashtagModel> CategoryIds { get; set; }
        public IEnumerable<CategoryHashtagModel> HashtagIds { get; set; }
    }
}
