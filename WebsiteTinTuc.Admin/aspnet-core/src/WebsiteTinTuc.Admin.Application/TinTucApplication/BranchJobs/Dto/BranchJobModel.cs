using System;

namespace WebsiteTinTuc.Admin.TinTucApplication.BranchJobs.Dto
{
    public class BranchJobModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BranchJobUrl { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
