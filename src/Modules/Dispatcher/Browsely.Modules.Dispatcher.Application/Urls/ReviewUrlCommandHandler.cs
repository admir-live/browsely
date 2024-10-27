using Browsely.Common.Application.Abstractions.Data;
using Browsely.Common.Application.Messaging;
using Browsely.Common.Domain;
using Browsely.Modules.Dispatcher.Domain.Url;
using Browsely.Modules.Dispatcher.Events;
using Microsoft.Extensions.Logging;

namespace Browsely.Modules.Dispatcher.Application.Urls;

internal sealed class ReviewUrlCommandHandler(
    IUrlRepository urlRepository,
    IUnitOfWork unitOfWork,
    IMessageBroker messageBroker,
    ILogger<ReviewUrlCommandHandler> logger)
    : ICommandHandler<ReviewUrlCommand, Ulid>
{
    private readonly ILogger<ReviewUrlCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMessageBroker _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IUrlRepository _urlRepository = urlRepository ?? throw new ArgumentNullException(nameof(urlRepository));

    public async Task<Result<Ulid>> Handle(ReviewUrlCommand request, CancellationToken cancellationToken)
    {
        Url? existingUrl = await _urlRepository.GetByUrlAsync(request.Uri, cancellationToken: cancellationToken);
        if (existingUrl is not null && existingUrl.InProcessingState())
        {
            _logger.LogInformation("URL already exists with ID: {UrlId}", existingUrl.Id);
            return Result.Failure<Ulid>(UrlErrors.InProcessingState(request.Uri));
        }

        var urlId = Ulid.NewUlid();
        var url = Url.Create(urlId, request.Uri);
        url.NextState();

        _urlRepository.Add(url);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _messageBroker.PublishAsync(
            new UrlReviewScheduledEvent(urlId, url.Uri),
            cancellationToken);

        _logger.LogInformation("URL review scheduled successfully for ID: {UrlId}", urlId);

        return Result.Success(urlId);
    }
}
