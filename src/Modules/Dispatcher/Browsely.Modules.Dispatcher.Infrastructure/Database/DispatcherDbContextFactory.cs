using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Browsely.Modules.Dispatcher.Infrastructure.Database;

public sealed class DispatcherDbContextFactory : IDesignTimeDbContextFactory<DispatcherDbContext>
{
    public DispatcherDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string? connectionString = configuration.GetConnectionString("DispatcherDatabase");

        var optionsBuilder = new DbContextOptionsBuilder<DispatcherDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new DispatcherDbContext(optionsBuilder.Options);
    }
}
