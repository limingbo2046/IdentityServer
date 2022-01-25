using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lcn.IdentityServer.UserAuths
{
    public interface IUserAuthAppService
    {
        Task<string> UserTokenAsync(UserTokenDto userTokenDto);
        Task<string> ClientTokenAsync(UserTokenDto userTokenDto);
        Task TestTenantUserInfo();
    }
}
