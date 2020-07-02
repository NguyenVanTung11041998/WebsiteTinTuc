using System;

namespace WebsiteTinTuc.Admin.TinTucApplication.Hashtags.Dto
{
    public class HashtagModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HashtagUrl { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
