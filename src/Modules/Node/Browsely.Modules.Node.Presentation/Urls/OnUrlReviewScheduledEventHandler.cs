using Browsely.Modules.Dispatcher.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Browsely.Modules.Node.Presentation.Urls;

public sealed class OnUrlReviewScheduledEventHandler(ILogger<OnUrlReviewScheduledEventHandler> logger) : Common.Application.Messaging.EventHandler<UrlReviewScheduledEvent>
{
    private readonly ILogger<OnUrlReviewScheduledEventHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public override Task Consume(ConsumeContext<UrlReviewScheduledEvent> context)
    {
        _logger.LogInformation("URL review scheduled successfully for ID: {UrlId}", context.Message.Url);
        return Task.CompletedTask;
    }
}
