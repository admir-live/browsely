namespace Browsely.Common.Application.Messaging;

/// <inheritdoc />
public abstract class EventHandler<TEvent> : IEventHandler<TEvent> where TEvent : IEvent
{
    /// <inheritdoc />
    public abstract Task Handle(TEvent @event, CancellationToken cancellationToken = default);

    /// <inheritdoc />
    public Task Handle(IEvent @event, CancellationToken cancellationToken = default)
    {
        return Handle((TEvent)@event, cancellationToken);
    }
}
