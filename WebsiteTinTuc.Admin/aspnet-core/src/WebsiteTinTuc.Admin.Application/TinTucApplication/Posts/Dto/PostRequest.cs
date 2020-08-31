using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using WebsiteTinTuc.Admin.Entities;

namespace WebsiteTinTuc.Admin.TinTucApplication.Posts.Dto
{
    [AutoMap(typeof(Post))]
    public class PostRequest
    {
        public Guid? Id { get; set; }
        [Required]
        [MinLength(6)]
        public string Title { get; set; }
        public string Content { get; set; }
        public long NumberOfViews { get; set; }
        public JobType JobType { get; set; }
        public long MinMoney { get; set; }
        public long MaxMoney { get; set; }
        public MoneyType MoneyType { get; set; }
        public int? TimeExperience { get; set; }
        public DateTime? EndDate { get; set; }
        public ExperienceType ExperienceType { get; set; }
        public Guid CompanyId { get; set; }
    }
}
