using System;

namespace WebsiteTinTuc.Admin.Models
{
    public class HashtagAgencyModel
    {
        public Guid Id { get; set; }
        public Guid AgencyId { get; set; }
        public Guid HashtagId { get; set; }
        public string AgencyName { get; set; }
        public string HashtagName { get; set; }
    }
}
