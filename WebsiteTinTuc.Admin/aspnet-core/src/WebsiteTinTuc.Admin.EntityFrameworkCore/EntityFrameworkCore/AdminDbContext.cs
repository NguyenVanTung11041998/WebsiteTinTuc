﻿using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebsiteTinTuc.Admin.Authorization.Roles;
using WebsiteTinTuc.Admin.Authorization.Users;
using WebsiteTinTuc.Admin.Entities;
using WebsiteTinTuc.Admin.MultiTenancy;

namespace WebsiteTinTuc.Admin.EntityFrameworkCore
{
    public class AdminDbContext : AbpZeroDbContext<Tenant, Role, User, AdminDbContext>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<AgencyPostHashtag> AgencyPostHashtags { get; set; }
        public DbSet<PostCounter> PostCounters { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<CV> CVs { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<BranchJob> BranchJobs { get; set; }
        public DbSet<BranchJobAgency> BranchJobAgencies { get; set; }
        public AdminDbContext(DbContextOptions<AdminDbContext> options)
            : base(options) { }
    }
}
