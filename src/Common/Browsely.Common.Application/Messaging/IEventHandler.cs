namespace Browsely.Common.Application.Messaging;

/// <summary>
///     Defines a handler for processing events.
/// </summary>
public interface IEventHandler
{
    /// <summary>
    ///     Handles the specified event.
    /// </summary>
    /// <param name="event">The event to handle.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Handle(IEvent @event, CancellationToken cancellationToken = default);
}

/// <summary>
///     Defines a handler for processing events of a specific type.
/// </summary>
/// <typeparam name="TEvent">The type of event to handle.</typeparam>
public interface IEventHandler<in TEvent> : IEventHandler
    where TEvent : IEvent
{
    /// <summary>
    ///     Handles the specified event.
    /// </summary>
    /// <param name="event">The event to handle.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Handle(TEvent @event, CancellationToken cancellationToken = default);
}
