using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using WebsiteTinTuc.Admin.Entities;

namespace WebsiteTinTuc.Admin.TinTucApplication.Companies.Dto
{
    [AutoMap(typeof(Company))]
    public class CompanyRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string LocationDescription { get; set; }
        public string Location { get; set; }
        public string DescrtionCompany { get; set; }
        public string Website { get; set; }
        public int? MinScale { get; set; }
        public string Treatment { get; set; }
        public int? MaxScale { get; set; }
        public string NationalityCompany { get; set; }
        public List<IFormFile> Files { get; set; }
        public IFormFile Thumbnail { get; set; }
        public List<Guid> HashtagIds { get; set; }
        public List<Guid> BranchJobCompanyIds { get; set; }
        public List<Guid> ImageDeletes { get; set; }
        public List<Guid> HashtagDeletes { get; set; }
        public List<Guid> BranchJobCompanyDeletes { get; set; }
    }
}
