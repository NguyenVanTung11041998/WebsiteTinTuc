using Abp.Authorization;
using WebsiteTinTuc.Admin.Authorization.Roles;
using WebsiteTinTuc.Admin.Authorization.Users;

namespace WebsiteTinTuc.Admin.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
