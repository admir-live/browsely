namespace Browsely.Common.Application.Messaging;

/// <inheritdoc />
public abstract class Event(Ulid id, DateTime occurredOnUtc) : IEvent
{
    public Ulid Id { get; init; } = id;
    public DateTime OccurredOnUtc { get; init; } = occurredOnUtc;
}
