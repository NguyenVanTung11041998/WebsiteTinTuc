using Abp.MultiTenancy;
using WebsiteTinTuc.Admin.Authorization.Users;

namespace WebsiteTinTuc.Admin.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
