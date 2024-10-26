namespace Browsely.Common.Application.Messaging;

/// <summary>
///     Interface for an event bus that allows publishing events asynchronously.
/// </summary>
public interface IEventBus
{
    /// <summary>
    ///     Publishes an event asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the event to be published. Must implement <see cref="IEvent" />.</typeparam>
    /// <param name="event">The event to be published.</param>
    /// <param name="cancellationToken">
    ///     A token to monitor for cancellation requests. Defaults to
    ///     <see cref="CancellationToken.None" />.
    /// </param>
    /// <returns>A task that represents the asynchronous publish operation.</returns>
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : IEvent;
}
