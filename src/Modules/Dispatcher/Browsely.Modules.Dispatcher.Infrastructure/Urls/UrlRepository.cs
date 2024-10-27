using Browsely.Modules.Dispatcher.Domain.Url;
using Browsely.Modules.Dispatcher.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Browsely.Modules.Dispatcher.Infrastructure.Urls;

public sealed class UrlRepository(DispatcherDbContext context) : IUrlRepository
{
    public void Add(Url url)
    {
        context.Url.Add(url);
    }

    public void Update(Url url)
    {
        context.Url.Update(url);
    }

    public Task<Url?> GetAsync(Ulid id, bool withTracking = false, CancellationToken cancellationToken = default)
    {
        var idAsGuid = id.ToGuid();

        IQueryable<Url> query = withTracking
            ? context.Url
            : context.Url.AsNoTracking();

        return query.FirstOrDefaultAsync(url => url.Id == idAsGuid, cancellationToken);
    }

    public Task<Url?> GetByUrlAsync(Uri url, bool withTracking = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Url> query = withTracking
            ? context.Url
            : context.Url.AsNoTracking();

        return query
            .OrderByDescending(u => u.ModifiedOnUtc)
            .FirstOrDefaultAsync(u => u.Uri == url, cancellationToken);
    }
}
