using Volo.Abp.Modularity;

namespace Lcn.IdentityServer
{
    [DependsOn(
        typeof(IdentityServerApplicationModule),
        typeof(IdentityServerDomainTestModule)
        )]
    public class IdentityServerApplicationTestModule : AbpModule
    {

    }
}