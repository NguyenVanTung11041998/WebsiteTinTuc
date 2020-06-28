﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WebsiteTinTuc.Admin.Configuration;
using WebsiteTinTuc.Admin.Web;

namespace WebsiteTinTuc.Admin.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class AdminDbContextFactory : IDesignTimeDbContextFactory<AdminDbContext>
    {
        public AdminDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AdminDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            AdminDbContextConfigurer.Configure(builder, configuration.GetConnectionString(AdminConsts.ConnectionStringName));

            return new AdminDbContext(builder.Options);
        }
    }
}
