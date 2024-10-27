using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Browsely.Common.Infrastructure.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigration<TDbContext>(this IApplicationBuilder app) where TDbContext : DbContext
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        try
        {
            context.Database.Migrate();
        }
        catch (Exception)
        {
            // Ignore due to docker compose latency. Need to add mechanism for graceful bootstrapping container.
        }
    }
}
