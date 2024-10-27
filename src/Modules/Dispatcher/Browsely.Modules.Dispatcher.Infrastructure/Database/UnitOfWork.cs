using Browsely.Common.Application.Abstractions.Data;

namespace Browsely.Modules.Dispatcher.Infrastructure.Database;

public sealed class DispatcherDbContextUnitOfWork(DispatcherDbContext context) : IUnitOfWork
{
    private readonly DispatcherDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
