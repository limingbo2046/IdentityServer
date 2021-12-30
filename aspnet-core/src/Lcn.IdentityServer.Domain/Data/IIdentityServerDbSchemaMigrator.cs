using System.Threading.Tasks;

namespace Lcn.IdentityServer.Data
{
    public interface IIdentityServerDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
