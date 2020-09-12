using Abp.MultiTenancy;
using System.Collections.Generic;

namespace WebsiteTinTuc.Admin.Authorization
{
    public class SystemPermission
    {
        public string Permission { get; set; }
        public MultiTenancySides MultiTenancySides { get; set; }
        public string DisplayName { get; set; }
        public bool IsConfiguration { get; set; }
        public static List<SystemPermission> ListPermissions = new List<SystemPermission>
        {
            new SystemPermission { Permission = PermissionNames.Pages_Tenants, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Tenants },
            new SystemPermission { Permission = PermissionNames.Pages_Roles, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Roles },
            new SystemPermission { Permission = PermissionNames.Pages_Users, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Users },
            new SystemPermission { Permission = PermissionNames.Pages_View_Company, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_View_Company },
            new SystemPermission { Permission = PermissionNames.Pages_Create_Company, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Create_Company },
            new SystemPermission { Permission = PermissionNames.Pages_Update_Company, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Update_Company },
            new SystemPermission { Permission = PermissionNames.Pages_Delete_Company, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Delete_Company },
            new SystemPermission { Permission = PermissionNames.Pages_Setting_Hot_Company, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Setting_Hot_Company },
            new SystemPermission { Permission = PermissionNames.Pages_View_Hashtag, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_View_Hashtag },
            new SystemPermission { Permission = PermissionNames.Pages_Create_Hashtag, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Create_Hashtag },
            new SystemPermission { Permission = PermissionNames.Pages_Update_Hashtag, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Update_Hashtag },
            new SystemPermission { Permission = PermissionNames.Pages_Delete_Hashtag, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Delete_Hashtag },
            new SystemPermission { Permission = PermissionNames.Pages_View_Post, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_View_Post },
            new SystemPermission { Permission = PermissionNames.Pages_Create_Post, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Create_Post },
            new SystemPermission { Permission = PermissionNames.Pages_Update_Post, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Update_Post },
            new SystemPermission { Permission = PermissionNames.Pages_Delete_Post, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Delete_Post },
            new SystemPermission { Permission = PermissionNames.Pages_View_Nationality, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_View_Nationality },
            new SystemPermission { Permission = PermissionNames.Pages_Create_Nationality, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Create_Nationality },
            new SystemPermission { Permission = PermissionNames.Pages_Update_Nationality, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Update_Nationality },
            new SystemPermission { Permission = PermissionNames.Pages_Delete_Nationality, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Delete_Nationality },
            new SystemPermission { Permission = PermissionNames.Pages_View_BranchJob, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_View_BranchJob },
            new SystemPermission { Permission = PermissionNames.Pages_Create_BranchJob, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Create_BranchJob },
            new SystemPermission { Permission = PermissionNames.Pages_Update_BranchJob, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Update_BranchJob },
            new SystemPermission { Permission = PermissionNames.Pages_Delete_BranchJob, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Delete_BranchJob },
            new SystemPermission { Permission = PermissionNames.Pages_View_Level, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_View_Level },
            new SystemPermission { Permission = PermissionNames.Pages_Create_Level, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Create_Level },
            new SystemPermission { Permission = PermissionNames.Pages_Update_Level, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Update_Level },
            new SystemPermission { Permission = PermissionNames.Pages_Delete_Level, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDisplayName.Pages_Delete_Level }
        };
    }
}
