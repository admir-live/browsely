using Browsely.Common.Application.Messaging;
using Browsely.Modules.Dispatcher.Application.Urls;
using Browsely.Modules.Dispatcher.Domain.Url;
using Browsely.Modules.Node.Events;
using MassTransit;

namespace Browsely.Modules.Dispatcher.Presentation.Urls;

public sealed class OnUrlReviewedEventHandler(IMessageBroker messageBroker) : Common.Application.Messaging.EventHandler<UrlReviewedEvent>
{
    private readonly IMessageBroker _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));

    public override Task Consume(ConsumeContext<UrlReviewedEvent> context)
    {
        return _messageBroker.SendAsync(
            new UpdateContentCommand(
                context.Message.Id,
                context.Message.StatusCode,
                new Payload(context.Message.Content)));
    }
}
