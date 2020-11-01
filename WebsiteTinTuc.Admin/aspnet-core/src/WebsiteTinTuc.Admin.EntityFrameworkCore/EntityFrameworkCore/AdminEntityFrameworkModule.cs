﻿using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebsiteTinTuc.Admin.Constants;
using WebsiteTinTuc.Admin.EntityFrameworkCore.Seed;

namespace WebsiteTinTuc.Admin.EntityFrameworkCore
{
    [DependsOn(
        typeof(AdminCoreModule),
        typeof(AbpZeroCoreEntityFrameworkCoreModule)
    )]
    public class AdminEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<AdminDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        AdminDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        AdminDbContextConfigurer.Configure(options.DbContextOptions, ConstantVariable.ConectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AdminEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
