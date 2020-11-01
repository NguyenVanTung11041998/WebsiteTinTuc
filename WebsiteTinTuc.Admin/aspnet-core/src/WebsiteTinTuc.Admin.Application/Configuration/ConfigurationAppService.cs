using Abp.Authorization;
using Abp.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebsiteTinTuc.Admin.Configuration.Dto;
using WebsiteTinTuc.Admin.Constants;

namespace WebsiteTinTuc.Admin.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : AdminAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }

        public void ChangeConnectionString(ConnectionStringModel input)
        {
            string connectionString = $@"Server={input.ServerName}; Database={input.DatabaseName}; Trusted_Connection=True;";
            if (input.IsAuthenticate)
                connectionString = $@"Data Source={input.ServerName};Initial Catalog={input.DatabaseName};User ID={input.UserName}; Password={input.Password}";

            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                ConstantVariable.ConectionString = connectionString;
                ConstantVariable.DatabaseName = input.DatabaseName;
                ConstantVariable.ServerName = input.ServerName;
                ConstantVariable.UserName = input.UserName;
                ConstantVariable.Password = input.Password;
            }
            catch
            {
                throw new UserFriendlyException("Kết nối database thất bại, hãy kiểm tra lại kết nối");
            }
        }

        public ConnectionStringModel GetConnectionString()
        {
            return new ConnectionStringModel
            {
                DatabaseName = ConstantVariable.DatabaseName,
                Password = ConstantVariable.Password,
                IsAuthenticate = !ConstantVariable.UserName.IsNullOrEmpty(),
                UserName = ConstantVariable.UserName,
                ServerName = ConstantVariable.ServerName
            };
        }
    }
}
