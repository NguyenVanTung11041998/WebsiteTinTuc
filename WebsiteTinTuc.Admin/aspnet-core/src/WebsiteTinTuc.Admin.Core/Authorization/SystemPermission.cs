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
            new SystemPermission { Permission = PermissionNames.Pages_Tenants, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDIsplayName.Pages_Tenants },
            new SystemPermission { Permission = PermissionNames.Pages_Roles, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDIsplayName.Pages_Roles },
            new SystemPermission { Permission = PermissionNames.Pages_Users, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDIsplayName.Pages_Users },
            new SystemPermission { Permission = PermissionNames.Pages_View_Agency, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDIsplayName.Pages_View_Agency },
            new SystemPermission { Permission = PermissionNames.Pages_Create_Agency, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDIsplayName.Pages_Create_Agency },
            new SystemPermission { Permission = PermissionNames.Pages_Update_Agency, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDIsplayName.Pages_Update_Agency },
            new SystemPermission { Permission = PermissionNames.Pages_Delete_Agency, MultiTenancySides = MultiTenancySides.Host, DisplayName = PermissionDIsplayName.Pages_Delete_Agency }
        };
    }
}
