using System.Collections.Generic;

namespace WebsiteTinTuc.Admin.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
