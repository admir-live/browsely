using Browsely.Common.Application.Messaging;
using Browsely.Modules.Dispatcher.Events;
using Browsely.Modules.Node.Application.Content;
using Browsely.Modules.Node.Application.Docker;
using Browsely.Modules.Node.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Browsely.Modules.Node.Presentation.Urls;

public sealed class OnUrlReviewScheduledEventHandler(
    IMessageBroker messageBroker,
    IContentService contentService,
    IDockerService dockerService,
    ILogger<OnUrlReviewScheduledEventHandler> logger)
    : Common.Application.Messaging.EventHandler<UrlReviewScheduledEvent>
{
    private readonly IContentService _contentService = contentService ?? throw new ArgumentNullException(nameof(contentService));
    private readonly IDockerService _dockerService = dockerService ?? throw new ArgumentNullException(nameof(dockerService));
    private readonly ILogger<OnUrlReviewScheduledEventHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMessageBroker _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));

    public override async Task Consume(ConsumeContext<UrlReviewScheduledEvent> context)
    {
        _logger.LogInformation("URL review scheduled successfully for ID: {UrlId}", context.Message.Url);

        await _dockerService.EnsureDockerContainerRunningAsync();
        ContentResponse content = await _contentService.GetContentAsync(new ContentRequest(context.Message.Url));
        await _dockerService.EnsureDockerContainerStoppedAsync();

        _logger.LogInformation("Content retrieved from URL: {StatusCode} :: {Content}", content.StatusCode, content.Content);

        await _messageBroker.PublishAsync(
            new UrlReviewedEvent(
                context.Message.Id,
                content.StatusCode,
                content.Content));
    }
}
