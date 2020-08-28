using System;
using System.ComponentModel.DataAnnotations;

namespace WebsiteTinTuc.Admin.TinTucApplication.BranchJobs.Dto
{
    public class BranchJobRequest
    {
        public Guid? Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
