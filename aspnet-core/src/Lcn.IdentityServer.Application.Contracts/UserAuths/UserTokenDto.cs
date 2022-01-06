using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;

namespace Lcn.IdentityServer.UserAuths
{
    public class UserTokenDto
    {
        public string UserName { get; set; }

        [DisableAuditing]
        public string UserPassword { get; set; }
        /// <summary>
        /// 用户能访问的API范围用空格隔开
        /// </summary>
        public string ApiScopes { get; set; }
    }
}
