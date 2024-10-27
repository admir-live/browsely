using System.Net;
using Browsely.Common.Application.Messaging;

namespace Browsely.Modules.Node.Events;

public sealed class UrlReviewedEvent(Ulid id, HttpStatusCode statusCode, string content) : IEvent
{
    public string Content { get; set; } = content;
    public HttpStatusCode StatusCode { get; set; } = statusCode;
    public Ulid Id { get; set; } = id;
    public DateTime OccurredOnUtc { get; set; } = DateTime.UtcNow;
}
