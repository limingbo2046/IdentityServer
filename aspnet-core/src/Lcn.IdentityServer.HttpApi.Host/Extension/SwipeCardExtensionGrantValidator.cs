using IdentityServer4.Validation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace Lcn.IdentityServer.Extension
{
    public class SwipeCardExtensionGrantValidator : IExtensionGrantValidator, ITransientDependency//由DI自动注入
    {
        protected IdentityUserManager IdentityUserManager;
        protected IConfiguration _Configuration;

        public SwipeCardExtensionGrantValidator(IdentityUserManager identityUserManager, IConfiguration configuration)
        {
            IdentityUserManager = identityUserManager;
            _Configuration = configuration;
        }

        public string GrantType => "SwipeCard";//自定义签证类型

        /// <summary>
        /// 验证签证的逻辑
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var cardNo = context.Request.Raw.Get("card_no");//在参数里面获取卡号//可以考虑加密
            if (cardNo != null)
            {
                var user = ValidataCardNo(cardNo);
                var subjectId = user?.Id.ToString();//找到用户ID

                if (subjectId != null)
                {
                    var claim = new List<System.Security.Claims.Claim>();
                    if (user.TenantId.HasValue)
                    {
                        claim.Add(new System.Security.Claims.Claim(Volo.Abp.Security.Claims.AbpClaimTypes.TenantId, user.TenantId.Value.ToString().ToLower()));
                    }
                    context.Result = new GrantValidationResult(subject: subjectId, authenticationMethod: "swipe_card", claims: claim, identityProvider: "card");//认证方法，刷卡
                }
                else
                {
                    context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidTarget, "该卡未绑定！");
                }
            }
            else
            {
                context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidTarget, "必须提供卡号！");
            }
            return Task.CompletedTask;
        }

        protected IdentityUser ValidataCardNo(string cardNo)
        {
            var cardTypes = _Configuration.GetSection("SwipeCardTypes").Value.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            foreach (var cardType in cardTypes)
            {
                var lstUser = IdentityUserManager.GetUsersForClaimAsync(new System.Security.Claims.Claim(cardType, cardNo)).Result;//因为卡号的硬件序号不应该被看见，所以不属于scope里面的才行
                if (lstUser.Count <= 0)
                {
                    continue;
                }
                return lstUser.FirstOrDefault();
            }
            return null;
        }
    }
}
