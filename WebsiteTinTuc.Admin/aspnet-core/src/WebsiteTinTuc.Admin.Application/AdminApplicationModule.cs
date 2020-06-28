using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WebsiteTinTuc.Admin.Authorization;

namespace WebsiteTinTuc.Admin
{
    [DependsOn(
        typeof(AdminCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class AdminApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<AdminAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(AdminApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
