using Browsely.Common.Domain;

namespace Browsely.Common.Application.Messaging;

/// <summary>
///     Represents a message broker that sends commands and queries.
/// </summary>
/// <seealso cref="IEventBus" />
public interface IMessageBroker : IEventBus
{
    /// <summary>
    ///     Sends a command and returns a <see cref="Result" />.
    /// </summary>
    /// <param name="command">The command to send.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the command.</returns>
    Task<Result> SendAsync(ICommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Sends a command and returns a <see cref="Result{TResponse}" />.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="command">The command to send.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the command.</returns>
    Task<Result<TResponse>> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Sends a query and returns a <see cref="Result{TResponse}" />.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="query">The query to send.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the query.</returns>
    Task<Result<TResponse>> SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
}
