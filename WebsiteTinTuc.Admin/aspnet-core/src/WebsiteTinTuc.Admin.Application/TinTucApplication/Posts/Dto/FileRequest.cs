using Microsoft.AspNetCore.Http;
using WebsiteTinTuc.Admin.Entities;

namespace WebsiteTinTuc.Admin.TinTucApplication.Posts.Dto
{
    public class FileRequest
    {
        public IFormFile File { get; set; }
        public FileType FileType { get; set; }
    }
}
