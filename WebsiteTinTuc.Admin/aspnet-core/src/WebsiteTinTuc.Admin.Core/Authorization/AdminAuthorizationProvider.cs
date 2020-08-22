using Abp.Authorization;
using Abp.Localization;

namespace WebsiteTinTuc.Admin.Authorization
{
    public class AdminAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            foreach (var item in SystemPermission.ListPermissions)
            {
                context.CreatePermission(item.Permission, L(item.DisplayName), multiTenancySides: item.MultiTenancySides);
            }
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AdminConsts.LocalizationSourceName);
        }
    }
}
