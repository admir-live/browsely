using Browsely.Common.Application.Messaging;

namespace Browsely.Modules.Dispatcher.Events;

public sealed class UrlReviewScheduledEvent(Ulid id, Uri url) : IEvent
{
    public Uri Url { get; set; } = url;

    public Ulid Id { get; set; } = id;

    public DateTime OccurredOnUtc { get; set; } = DateTime.UtcNow;
}
