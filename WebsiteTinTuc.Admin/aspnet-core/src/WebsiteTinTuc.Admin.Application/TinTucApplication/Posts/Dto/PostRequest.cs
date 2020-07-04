using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebsiteTinTuc.Admin.TinTucApplication.Posts.Dto
{
    public class PostRequest
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [MinLength(6, ErrorMessage = "Tiêu đề phải ít nhất 6 ký tự")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string Description { get; set; }
        public List<FileRequest> Files { get; set; }
        public List<Guid> CategoryIds { get; set; }
        public List<Guid> HashtagIds { get; set; }
        public List<Guid> FileIdDelete { get; set; }
        public List<Guid> CategoryIdDelete { get; set; }
        public List<Guid> HashtagIdDelete { get; set; }
    }
}
