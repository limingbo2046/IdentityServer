using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;
using Microsoft.Extensions.Logging;

namespace Lcn.IdentityServer
{
    public class KeepLiveWorker : AsyncPeriodicBackgroundWorkerBase
    {
        protected UserAuths.IUserAuthAppService _UserAuthAppService;
        public KeepLiveWorker(AbpAsyncTimer timer,
             IServiceScopeFactory serviceScopeFactory, UserAuths.IUserAuthAppService userAuthAppService) : base(timer, serviceScopeFactory)
        {
            _UserAuthAppService = userAuthAppService;
            Timer.Period =3600000;//1小时
        }
        protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            Logger.LogDebug("运行请求");
            await _UserAuthAppService.UserTokenAsync(new UserAuths.UserTokenDto { UserName = "test" + new Random().Next(1, 1000).ToString(), UserPassword = "1234556" });
        }
    }
}
