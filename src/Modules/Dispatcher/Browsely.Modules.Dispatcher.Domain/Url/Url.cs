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
    public IUrlState CurrentState { get; private set; } = new ScheduledState();
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

    public void NextState()
    {
        CurrentState = CurrentState.Next();
        UpdateModifiedTimestamp();
    }

    public void Fail()
    {
        CurrentState = new FailedState();
        UpdateModifiedTimestamp();
    }

    public static Url Create(Ulid urlId, Uri requestUri)
    {
        var url = new Url(urlId);
        url.UpdateUri(requestUri);
        return url;
    }

    public bool InProcessingState()
    {
        return CurrentState is InReviewState or ScheduledState;
    }
}
