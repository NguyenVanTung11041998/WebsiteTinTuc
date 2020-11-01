using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Configuration.Dto;

namespace WebsiteTinTuc.Admin.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
        void ChangeConnectionString(ConnectionStringModel input);
        ConnectionStringModel GetConnectionString();
        ConnectionStringModel ResetConnectionStringDefault();
    }
}
