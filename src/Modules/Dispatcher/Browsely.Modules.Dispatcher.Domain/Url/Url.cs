using Browsely.Common.Domain;

namespace Browsely.Modules.Dispatcher.Domain.Url;

public sealed class Url : Entity
{
    public Url(Ulid id) : base(id)
    {
    }

    public Url() // Required for EF Core
    {
    }

    public Payload HtmlContent { get; private set; }
    public IUrlState CurrentState { get; } = new ScheduledState();
    public Uri Uri { get; private set; }

    public void UpdateHtmlContent(Payload htmlContent)
    {
        HtmlContent = htmlContent;
        UpdateModifiedTimestamp();
    }

    public void UpdateUri(Uri uri)
    {
        Uri = uri;
        UpdateModifiedTimestamp();
    }

    public void UpdateModifiedTimestamp()
    {
        ModifiedOnUtc = DateTime.UtcNow;
    }
}
