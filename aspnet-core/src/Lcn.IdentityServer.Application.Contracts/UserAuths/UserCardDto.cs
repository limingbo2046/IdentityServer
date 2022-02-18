using System;
using System.Collections.Generic;
using System.Text;

namespace Lcn.IdentityServer.UserAuths
{
   public class UserCardDto
    {
        public string Scopes { get; set; }
        public Guid? TenantId { get; set; }
        public string CardNo { get; set; }
    }
}
