using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AbpIssue.Data;

public class AbpIssueDbContextFactory : IDesignTimeDbContextFactory<AbpIssueDbContext>
{
    public AbpIssueDbContext CreateDbContext(string[] args)
    {

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<AbpIssueDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new AbpIssueDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
