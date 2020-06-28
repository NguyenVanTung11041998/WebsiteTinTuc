using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using WebsiteTinTuc.Admin.Authorization.Users;
using WebsiteTinTuc.Admin.Editions;

namespace WebsiteTinTuc.Admin.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
