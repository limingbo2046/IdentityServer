﻿using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Lcn.IdentityServer.UserAuths
{
    public class UserAuthAppService : IdentityServerAppService, IUserAuthAppService
    {
        private readonly IdentityUserManager _userManager;
        /// <summary>
        /// 配置
        /// </summary>
        protected IConfiguration Configuration { get; }
        /// <summary>
        /// 认证服务
        /// </summary>
        //protected IIdentityModelAuthenticationService AuthenticationService { get; }
        //private readonly IRepository<UserScopes.UserApiScope> _userApiScopesRepo;
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="_configuration"></param>
        /// <param name="authenticationService"></param>
        public UserAuthAppService(
            IConfiguration _configuration,
            IdentityUserManager userManager
            //IRepository<UserScopes.UserApiScope> userApiScopes
            )
        {
            Configuration = _configuration;
            _userManager = userManager;
            //_userApiScopesRepo = userApiScopes;
        }

        public async Task<string> UserTokenAsync(UserTokenDto userTokenDto)
        {
            //await CheckPolicyAsync(_权限.Store<DemandPlan>().创建);

            // TODO 传递用户名的时候，如何对参数进行加密？

            var user = await CheckUserNameAsync(userTokenDto);//把用户名检查补全6位数

            //通过apiScope来获取对应的Service的token,这样，才能调用该api的服务，所以该账号是所有admin的账号，通过scope来限定？那么不是同一个账号行不行呢？

            string accessToken;//令牌

            //var apiScopes = from api in _userApiScopesRepo where api.UserId == user.Id select api;
            //var lstApiScope = await AsyncExecuter.ToListAsync(apiScopes);
            //var scope = string.Join(' ', lstApiScope.Select(p => p.ApiScopeName).ToList());//能访问的api的scope
            //scope += " role";//
            //if (string.IsNullOrWhiteSpace(scope))
            //{
            //    Logger.LogError($"该用户{user.Id}没有访问任何api资源的权限！");
            //    throw new UserFriendlyException($"您没有访问任何api资源的权限！");
            //}

            accessToken = await RequestTokenWithUserAsync(user.UserName, userTokenDto.UserPassword, userTokenDto.ApiScopes);

            return accessToken;

        }

        /// <summary>
        /// 客户端的令牌（需要给客户端授权）
        /// </summary>
        /// <param name="userTokenDto"></param>
        /// <returns></returns>
        public async Task<string> ClientTokenAsync(UserTokenDto userTokenDto)
        {
            //await CheckPolicyAsync(_权限.Store<DemandPlan>().创建);

            // TODO 传递用户名的时候，如何对参数进行加密？

            var user = await CheckUserNameAsync(userTokenDto);//把用户名检查补全6位数

            //通过apiScope来获取对应的Service的token,这样，才能调用该api的服务，所以该账号是所有admin的账号，通过scope来限定？那么不是同一个账号行不行呢？

            string accessToken;//令牌 认证类型为联合认证Federation

            //var apiScopes = from api in _userApiScopesRepo where api.UserId == user.Id select api;
            //var lstApiScope = await AsyncExecuter.ToListAsync(apiScopes);
            //var scope = string.Join(' ', lstApiScope.Select(p => p.ApiScopeName).ToList());//能访问的api的scope

            //if (string.IsNullOrWhiteSpace(scope))
            //{
            //    Logger.LogError($"该用户{user.Id}没有访问任何api资源的权限！");
            //    throw new UserFriendlyException($"您没有访问任何api资源的权限！");
            //}

            accessToken = await RequestTokenAsync(user.UserName, userTokenDto.UserPassword, userTokenDto.ApiScopes);

            return accessToken;

        }

        private async Task<IdentityUser> CheckUserNameAsync(UserTokenDto userToken)
        {
            IdentityUser user = null;
            if (!userToken.UserName.Contains("@"))//非邮件的方式
            {
                user = await _userManager.FindByNameAsync(userToken.UserName);
                if (user == null)
                {
                    var newUserName = userToken.UserName.PadLeft(6, '0');//填充工号的0
                    user = await _userManager.FindByNameAsync(newUserName);//
                }
            }
            else
            {
                user = await _userManager.FindByEmailAsync(userToken.UserName);
                if (user == null)
                {
                    var newUserName = userToken.UserName.PadLeft(6, '0');//填充工号的0
                    user = await _userManager.FindByEmailAsync(newUserName);//
                }
            }
            if (user == null)
            {
                throw new UserFriendlyException($"找不到该账号{userToken.UserName}！");
            }

            return user;

        }

        protected async Task<string> RequestTokenAsync(string username, string password, string apiScope)
        {
            var authServer = Configuration.GetSection("AuthServer");
            if (authServer == null)
            {
                //配置不能为空
                Logger.LogError($"获取用户令牌出错，未配置认证服务器信息");
                throw new UserFriendlyException("获取用户令牌出错，未配置认证服务器信息,请联系管理员！");
            }
            string authority = authServer["Authority"];

            var client = GetHttpClient(authority);
            var disco = await client.GetDiscoveryDocumentAsync();//请求服务端口https://localhost:9009/.well-known/openid-configuration

            if (disco.IsError)
            {
                Logger.LogError($"发现IS4文档服务出错{disco.Error}");
                throw new UserFriendlyException("客户端认证出错，请按CTRL+F5刷新后重试！");
            }

            var pwdRequest = new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,//请求令牌节点
                ClientId = authServer["ClientId"],
                ClientSecret = authServer["ClientSecret"],
                UserName = username,
                Password = password,
                Scope = apiScope,//如果要获取用户信息，需要在这里添加用户的scope
                //Parameters =
                //{
                //    { "acr_values", "tenant:custom_account_store1 foo bar quux" }
                //}
            };

            var response = await client.RequestPasswordTokenAsync(pwdRequest);//TODO，该登录方式是客户端登录，不是用户登录？所以在其他程序中没有该用户认证的过程，没有对应的用户策略，认证信息

            if (response.IsError)
            {
                Logger.LogError($"请求密码令牌出错{response.Error}");
                throw new UserFriendlyException("客户端认证出错，请按CTRL+F5刷新后重试！");
            }

            return response.AccessToken;

        }

        protected async Task<string> RequestTokenWithUserAsync(string username, string password, string apiScope)
        {
            var authServer = Configuration.GetSection("AuthServer");
            if (authServer == null)
            {
                //配置不能为空
                Logger.LogError($"获取用户令牌出错，未配置认证服务器信息");
                throw new UserFriendlyException("获取用户令牌出错，未配置认证服务器信息,请联系管理员！");
            }
            string authority = authServer["Authority"];

            var client = GetHttpClient(authority);
            var disco = await client.GetDiscoveryDocumentAsync();//请求服务端口https://localhost:9009/.well-known/openid-configuration

            if (disco.IsError)
            {
                Logger.LogError($"发现IS4文档服务出错{disco.Error}");
                throw new UserFriendlyException("客户端认证出错，请按CTRL+F5刷新后重试！");
            }
            apiScope = string.Concat(apiScope, " ", authServer["Scopes"]);
            var pwdRequest = new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,//请求令牌节点
                ClientId = authServer["ClientId"],
                ClientSecret = authServer["ClientSecret"],
                UserName = username,
                Password = password,
                Scope = apiScope,//如果要获取用户信息，需要在这里添加用户的scope
                //Parameters =
                //{
                //    { "acr_values", "tenant:custom_account_store1 foo bar quux" }
                //}
            };

            var response = await client.RequestPasswordTokenAsync(pwdRequest);

            if (response.IsError)
            {
                Logger.LogError($"用户{username}请求密码令牌出错{response.Error}");
                throw new UserFriendlyException($"用户请求令牌出错！错误信息{response.ErrorDescription}");
            }
            string raw = response.Raw;//未加密
            //TODO 这里是拿令牌访问用户信息而已，令牌的认证方式已经固定了。
            client = GetHttpClient(authority, response.AccessToken);

            var usercache = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = disco.UserInfoEndpoint,
                Token = response.AccessToken
            });

            if (usercache.IsError) throw new Exception(usercache.Error);

            foreach (var c in usercache.Claims)
            {
                Console.WriteLine("输出用户信息{0},{1}", c.Type, c.Value);
            }

            return response.AccessToken;

        }

        protected HttpClient GetHttpClient(string authority, string bearer = null)
        {
            var clientHandler = new HttpClientHandler()//ABP的client模块没有这个，用的是厂家创建的
            {
                ServerCertificateCustomValidationCallback = delegate { return true; }//自动过滤https证书要求
            };

            var client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(authority);
            if (!string.IsNullOrWhiteSpace(bearer))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearer);
            }
            return client;

        }

    }
}