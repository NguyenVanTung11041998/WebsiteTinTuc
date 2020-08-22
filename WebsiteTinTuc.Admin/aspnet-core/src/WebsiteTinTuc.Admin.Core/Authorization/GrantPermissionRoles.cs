using System.Collections.Generic;
using static WebsiteTinTuc.Admin.Authorization.Roles.StaticRoleNames;

namespace WebsiteTinTuc.Admin.Authorization
{
    public class GrantPermissionRoles
    {
        public static Dictionary<string, List<string>> PermissionRoles = new Dictionary<string, List<string>>
        {
            {
                Host.Admin,
                new List<string>
                {
                    PermissionNames.Pages_Tenants,
                    PermissionNames.Pages_Roles,
                    PermissionNames.Pages_Users,
                    PermissionNames.Pages_View_Agency,
                    PermissionNames.Pages_Create_Agency,
                    PermissionNames.Pages_Update_Agency,
                    PermissionNames.Pages_Delete_Agency
                }
            },
            {
                Host.Hr,
                new List<string>
                {
                    PermissionNames.Pages_View_Agency,
                    PermissionNames.Pages_Create_Agency,
                    PermissionNames.Pages_Update_Agency,
                    PermissionNames.Pages_Delete_Agency
                }
            },
            {
                Host.User,
                new List<string>
                {

                }
            }
        };
    }
}
