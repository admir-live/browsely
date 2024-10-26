using Browsely.Common.Application.Messaging;
using Browsely.Common.Domain;
using MassTransit;
using MediatR;

namespace BrowselyCommon.Infrastructure.Messaging;

/// <inheritdoc />
internal sealed class DefaultMessageBroker(IMediator mediator, IBus bus) : IMessageBroker
{
    private readonly IBus _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <inheritdoc />
    public async Task<Result> SendAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Result<TResponse>> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Result<TResponse>> SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(query, cancellationToken);
    }

    /// <inheritdoc />
    public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : IEvent
    {
        return _bus.Publish(@event, cancellationToken);
    }
}
