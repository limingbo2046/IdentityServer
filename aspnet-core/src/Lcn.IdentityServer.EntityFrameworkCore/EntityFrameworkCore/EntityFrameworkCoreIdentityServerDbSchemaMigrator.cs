using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lcn.IdentityServer.Data;
using Volo.Abp.DependencyInjection;

namespace Lcn.IdentityServer.EntityFrameworkCore
{
    public class EntityFrameworkCoreIdentityServerDbSchemaMigrator
        : IIdentityServerDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreIdentityServerDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the IdentityServerDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<IdentityServerDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
