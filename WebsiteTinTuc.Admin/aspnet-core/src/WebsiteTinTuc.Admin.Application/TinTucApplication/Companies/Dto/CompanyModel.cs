using System;
using System.Collections.Generic;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.TinTucApplication.Companies.Dto
{
    public class CompanyModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsHot { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string LocationDescription { get; set; }
        public string Location { get; set; }
        public string FullNameCompany { get; set; }
        public string Website { get; set; }
        public int? MinScale { get; set; }
        public string Treatment { get; set; }
        public int? MaxScale { get; set; }
        public Guid NationalityId { get; set; }
        public string NationalityCompany { get; set; }
        public string CompanyUrl { get; set; }
        public long UserId { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreatorName { get; set; }
        public ObjectFile Thumbnail { get; set; }
        public ObjectFile NationalityImage { get; set; }
        public IEnumerable<ObjectFile> Images { get; set; }
        public IEnumerable<HashtagCompanyPostModel> Hashtags { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<BranchJobCompanyModel> BranchJobCompanies { get; set; }
    }
}
