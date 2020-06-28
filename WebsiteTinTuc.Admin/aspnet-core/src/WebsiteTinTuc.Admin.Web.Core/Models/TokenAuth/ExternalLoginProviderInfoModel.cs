using Abp.AutoMapper;
using WebsiteTinTuc.Admin.Authentication.External;

namespace WebsiteTinTuc.Admin.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
