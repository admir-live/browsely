namespace Browsely.Common.Domain;

public interface IDomainEvent
{
    Ulid Id { get; }

    DateTime OccurredOnUtc { get; }
}
