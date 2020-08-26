using System;
using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.TinTucApplication.Nationalities.Dto
{
    public class NationalityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ObjectFile Image { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
