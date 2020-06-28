using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using WebsiteTinTuc.Admin.Configuration.Dto;

namespace WebsiteTinTuc.Admin.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : AdminAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
