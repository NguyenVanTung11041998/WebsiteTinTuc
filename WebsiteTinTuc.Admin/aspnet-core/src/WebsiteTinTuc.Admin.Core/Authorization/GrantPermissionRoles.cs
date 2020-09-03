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
                    PermissionNames.Pages_View_Company,
                    PermissionNames.Pages_Create_Company,
                    PermissionNames.Pages_Update_Company,
                    PermissionNames.Pages_Delete_Company,
                    PermissionNames.Pages_View_Hashtag,
                    PermissionNames.Pages_Create_Hashtag,
                    PermissionNames.Pages_Update_Hashtag,
                    PermissionNames.Pages_Delete_Hashtag,
                    PermissionNames.Pages_View_Post,
                    PermissionNames.Pages_Create_Post,
                    PermissionNames.Pages_Update_Post,
                    PermissionNames.Pages_Delete_Post,
                    PermissionNames.Pages_View_Nationality,
                    PermissionNames.Pages_Create_Nationality,
                    PermissionNames.Pages_Update_Nationality,
                    PermissionNames.Pages_Delete_Nationality,
                    PermissionNames.Pages_View_BranchJob,
                    PermissionNames.Pages_Create_BranchJob,
                    PermissionNames.Pages_Update_BranchJob,
                    PermissionNames.Pages_Delete_BranchJob,
                    PermissionNames.Pages_View_Level,
                    PermissionNames.Pages_Create_Level,
                    PermissionNames.Pages_Update_Level,
                    PermissionNames.Pages_Delete_Level
                }
            },
            {
                Host.Hr,
                new List<string>
                {
                    PermissionNames.Pages_View_Company,
                    PermissionNames.Pages_Create_Company,
                    PermissionNames.Pages_Update_Company,
                    PermissionNames.Pages_View_Hashtag,
                    PermissionNames.Pages_Create_Hashtag,
                    PermissionNames.Pages_Update_Hashtag,
                    PermissionNames.Pages_View_Post,
                    PermissionNames.Pages_Create_Post,
                    PermissionNames.Pages_Update_Post,
                    PermissionNames.Pages_View_Nationality,
                    PermissionNames.Pages_View_BranchJob,
                    PermissionNames.Pages_View_Level
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
