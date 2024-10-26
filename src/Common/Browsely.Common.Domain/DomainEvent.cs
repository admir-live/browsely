namespace Browsely.Common.Domain;

public abstract class DomainEvent(Ulid id, DateTime occurredOnUtc) : IDomainEvent
{
    protected DomainEvent() : this(Ulid.NewUlid(), DateTime.UtcNow)
    {
    }

    public Ulid Id { get; init; } = id;

    public DateTime OccurredOnUtc { get; init; } = occurredOnUtc;
}
