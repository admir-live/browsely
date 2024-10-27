using MassTransit;

namespace Browsely.Common.Application.Messaging;

/// <summary>
///     Represents an event handler that consumes events of type <typeparamref name="TEvent" />.
/// </summary>
/// <typeparam name="TEvent">The type of event to handle.</typeparam>
public interface IEventHandler<in TEvent> : IConsumer<TEvent> where TEvent : class, IEvent
{
}
