using Volo.Abp.Settings;

namespace Lcn.IdentityServer.Settings
{
    public class IdentityServerSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(IdentityServerSettings.MySetting1));
        }
    }
}
