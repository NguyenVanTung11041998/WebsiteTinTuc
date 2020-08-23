using System;
using System.Collections.Generic;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.TinTucApplication.Agencies.Dto
{
    public class AgencyModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string LocationDescription { get; set; }
        public string Location { get; set; }
        public string DescrtionAgency { get; set; }
        public string Website { get; set; }
        public int? MinScale { get; set; }
        public string Treatment { get; set; }
        public int? MaxScale { get; set; }
        public string NationalityAgency { get; set; }
        public string AgencyUrl { get; set; }
        public long UserId { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreatorName { get; set; }
        public ObjectFile Thumbnail { get; set; }
        public ObjectFile NationalityImage { get; set; }
        public IEnumerable<ObjectFile> Images { get; set; }
        public IEnumerable<HashtagAgencyModel> Hashtags { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<BranchJobAgencyModel> BranchJobAgencies { get; set; }
    }
}
