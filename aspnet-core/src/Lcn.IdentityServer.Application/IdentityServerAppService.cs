using System;
using System.Collections.Generic;
using System.Text;
using Lcn.IdentityServer.Localization;
using Volo.Abp.Application.Services;

namespace Lcn.IdentityServer
{
    /* Inherit your application services from this class.
     */
    public abstract class IdentityServerAppService : ApplicationService
    {
        protected IdentityServerAppService()
        {
            LocalizationResource = typeof(IdentityServerResource);
        }
    }
}
