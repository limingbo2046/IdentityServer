using Lcn.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Lcn.IdentityServer
{
    [DependsOn(
        typeof(IdentityServerEntityFrameworkCoreTestModule)
        )]
    public class IdentityServerDomainTestModule : AbpModule
    {

    }
}