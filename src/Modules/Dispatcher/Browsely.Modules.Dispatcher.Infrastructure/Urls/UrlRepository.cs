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

    public Task<Url?> GetAsync(Ulid id, CancellationToken cancellationToken = default)
    {
        var idAsGuid = id.ToGuid();
        return context.Url
            .AsNoTracking()
            .FirstOrDefaultAsync(url => url.Id == idAsGuid, cancellationToken);
    }
}
