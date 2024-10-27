using System.Net;
using Browsely.Common.Application.Abstractions.Data;
using Browsely.Common.Application.Messaging;
using Browsely.Common.Domain;
using Browsely.Modules.Dispatcher.Domain.Url;
using Microsoft.Extensions.Logging;

namespace Browsely.Modules.Dispatcher.Application.Urls;

internal sealed class UpdateContentCommandHandler(
    ILogger<UpdateContentCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IUrlRepository urlRepository)
    : ICommandHandler<UpdateContentCommand>
{
    private readonly ILogger<UpdateContentCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IUrlRepository _urlRepository = urlRepository ?? throw new ArgumentNullException(nameof(urlRepository));

    public async Task<Result> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
    {
        Url? url = await _urlRepository.GetAsync(request.Id, true, cancellationToken);
        if (url == null)
        {
            return LogAndReturn(
                LogLevel.Warning,
                "URL not found for ID: {UrlId}",
                request.Id,
                Result.Failure(UrlErrors.NotExists(request.Id))
            );
        }

        url.UpdateHtmlContent(request.Content);

        if (request.StatusCode != HttpStatusCode.OK)
        {
            url.Fail();
            return await SaveAndLogAsync("URL content update failed for ID: {UrlId}", request.Id, cancellationToken);
        }

        url.NextState();
        return await SaveAndLogAsync("URL content updated successfully for ID: {UrlId}", request.Id, cancellationToken);
    }

    private Result LogAndReturn(LogLevel logLevel, string message, Ulid urlId, Result result)
    {
        _logger.Log(logLevel, message, urlId);
        return result;
    }

    private async Task<Result> SaveAndLogAsync(string message, Ulid urlId, CancellationToken cancellationToken)
    {
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation(message, urlId);
        return Result.Success();
    }
}
