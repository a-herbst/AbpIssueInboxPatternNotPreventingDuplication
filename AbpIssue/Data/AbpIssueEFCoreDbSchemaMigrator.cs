using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace AbpIssue.Data;

public class AbpIssueEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public AbpIssueEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the AbpIssueDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<AbpIssueDbContext>()
            .Database
            .MigrateAsync();
    }
}
