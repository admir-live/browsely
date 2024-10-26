namespace Browsely.Common.Domain;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected Entity()
    {
    }

    protected Entity(Ulid id) : this()
    {
        Id = id.ToGuid();
    }

    public Guid Id { get; init; }
    public DateTime CreatedOnUtc { get; protected set; } = DateTime.UtcNow;
    public DateTime ModifiedOnUtc { get; protected set; } = DateTime.UtcNow;

    public IReadOnlyCollection<IDomainEvent> DomainEvents => [.. _domainEvents];

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
