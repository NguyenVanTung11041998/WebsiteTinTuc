using System;

namespace WebsiteTinTuc.Admin.TinTucApplication.Hashtags.Dto
{
    public class HashtagRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public bool IsHot { get; set; }
    }
}
