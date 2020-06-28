using System;

namespace WebsiteTinTuc.Admin.TinTucApplication.Categories.Dto
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CategoryUrl { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
