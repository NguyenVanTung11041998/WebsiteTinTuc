using System;

namespace WebsiteTinTuc.Admin.Models
{
    public class BranchJobAgencyModel
    {
        public Guid Id { get; set; }
        public Guid AgencyId { get; set; }
        public Guid BranchJobId { get; set; }
        public string AgencyName { get; set; }
        public string BranchJobName { get; set; }
    }
}
