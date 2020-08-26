using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebsiteTinTuc.Admin.TinTucApplication.Nationalities.Dto
{
    public class NationalityRequest
    {
        public Guid? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
