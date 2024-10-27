using Browsely.Modules.Dispatcher.Domain.Url;
using Browsely.Modules.Dispatcher.Infrastructure.Urls;
using Microsoft.EntityFrameworkCore;

namespace Browsely.Modules.Dispatcher.Infrastructure.Database;

public sealed class DispatcherDbContext(DbContextOptions<DispatcherDbContext> options) : DbContext(options)
{
    internal DbSet<Url> Url { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UrlEntityTypeConfiguration());
    }
}
