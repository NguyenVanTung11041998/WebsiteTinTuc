using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using WebsiteTinTuc.Admin.Authorization.Users;
using WebsiteTinTuc.Admin.MultiTenancy;
using WebsiteTinTuc.Admin.IoC;
using Abp.Dependency;

namespace WebsiteTinTuc.Admin
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class AdminAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        public IWorkScope WorkScope { get; set; }

        protected AdminAppServiceBase()
        {
            WorkScope = IocManager.Instance.Resolve<IWorkScope>();
            LocalizationSourceName = AdminConsts.ConnectionStringName;
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
