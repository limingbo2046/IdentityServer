using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Lcn.IdentityServer
{
    [Dependency(ReplaceServices = true)]
    public class IdentityServerBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "认证服务";
    }
}
