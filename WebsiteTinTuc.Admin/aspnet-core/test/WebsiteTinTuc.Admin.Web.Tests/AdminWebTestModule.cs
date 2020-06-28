using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WebsiteTinTuc.Admin.EntityFrameworkCore;
using WebsiteTinTuc.Admin.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace WebsiteTinTuc.Admin.Web.Tests
{
    [DependsOn(
        typeof(AdminWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class AdminWebTestModule : AbpModule
    {
        public AdminWebTestModule(AdminEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AdminWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(AdminWebMvcModule).Assembly);
        }
    }
}