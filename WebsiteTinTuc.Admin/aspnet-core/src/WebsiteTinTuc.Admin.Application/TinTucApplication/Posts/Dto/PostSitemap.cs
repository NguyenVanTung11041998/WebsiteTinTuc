using System;

namespace WebsiteTinTuc.Admin.TinTucApplication.Posts.Dto
{
    public class PostSitemap
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string PostUrl { get; set; }
        public string AgencyName { get; set; }
        public bool IsCreate { get; set; }
    }
}
