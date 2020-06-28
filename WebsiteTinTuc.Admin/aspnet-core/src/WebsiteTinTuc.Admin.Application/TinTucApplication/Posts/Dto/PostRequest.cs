using System;
using System.Collections.Generic;

namespace WebsiteTinTuc.Admin.TinTucApplication.Posts.Dto
{
    public class PostRequest
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public List<FileRequest> Files { get; set; }
        public List<Guid> CategoryIds { get; set; }
        public List<Guid> HashtagIds { get; set; }
        public List<Guid> FileIdDelete { get; set; }
        public List<Guid> CategoryIdDelete { get; set; }
        public List<Guid> HashtagIdDelete { get; set; }
    }
}
