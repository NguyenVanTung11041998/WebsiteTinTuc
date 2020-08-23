using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using WebsiteTinTuc.Admin.Entities;

namespace WebsiteTinTuc.Admin.TinTucApplication.Agencies.Dto
{
    [AutoMap(typeof(Agency))]
    public class AgencyRequest
    {
        public Guid? Id { get; set; }
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
        public List<IFormFile> Files { get; set; }
        public IFormFile Thumbnail { get; set; }
        public IFormFile NationalityImage { get; set; }
        public List<Guid> HashtagIds { get; set; }
        public List<Guid> BranchJobAgencyIds { get; set; }
        public List<Guid> ImageDeletes { get; set; }
        public List<Guid> HashtagDeletes { get; set; }
        public List<Guid> BranchJobAgencyDeletes { get; set; }
    }
}
