using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WebsiteTinTuc.Admin.Configuration;

namespace WebsiteTinTuc.Admin.Web.Host.Startup
{
    [DependsOn(
       typeof(AdminWebCoreModule))]
    public class AdminWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public AdminWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AdminWebHostModule).GetAssembly());
        }
    }
}
