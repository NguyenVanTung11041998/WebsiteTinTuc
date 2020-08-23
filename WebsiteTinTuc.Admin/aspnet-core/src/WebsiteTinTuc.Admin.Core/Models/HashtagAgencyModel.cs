using System;

namespace WebsiteTinTuc.Admin.Models
{
    public class HashtagCompanyModel
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid HashtagId { get; set; }
        public string CompanyName { get; set; }
        public string HashtagName { get; set; }
    }
}
