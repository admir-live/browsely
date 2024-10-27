using MassTransit;

namespace Browsely.Common.Application.Messaging;

/// <inheritdoc />
public abstract class EventHandler<TEvent> : IEventHandler<TEvent> where TEvent : class, IEvent
{
    /// <inheritdoc />
    public abstract Task Consume(ConsumeContext<TEvent> context);
}
