using System;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.TinTucApplication.Posts.Dto
{
    public class PostPagingRequest : PageRequest
    {
        public string CompanyName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? HashtagId { get; set; }
        public JobType? JobType { get; set; }
    }
}
