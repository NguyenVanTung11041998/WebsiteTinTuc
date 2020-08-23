using System;

namespace WebsiteTinTuc.Admin.Models
{
    public class BranchJobCompanyModel
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid BranchJobId { get; set; }
        public string CompanyName { get; set; }
        public string BranchJobName { get; set; }
    }
}
