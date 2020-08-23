﻿using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebsiteTinTuc.Admin.Authorization.Users;
using WebsiteTinTuc.Admin.Models;

namespace WebsiteTinTuc.Admin.Entities
{
    public class Agency : FullAuditedEntity<Guid>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string LocationDescription { get; set; }
        public string Location { get; set; }
        public string DescrtionAgency { get; set; }
        public string Website { get; set; }
        public int? MinScale { get; set; }
        public string Treatment { get; set; }
        public int? MaxScale { get; set; }
        public string NationalityAgency { get; set; }
        public string AgencyUrl { get; set; }
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
        public virtual ICollection<AgencyPostHashtag> AgencyPostHashtags { get; set; }
        public virtual ICollection<BranchJobAgency> BranchJobAgencies { get; set; }
    }
}
