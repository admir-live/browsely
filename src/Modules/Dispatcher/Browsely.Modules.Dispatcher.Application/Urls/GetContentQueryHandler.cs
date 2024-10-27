using Browsely.Common.Application.Messaging;
using Browsely.Common.Domain;
using Browsely.Modules.Dispatcher.Domain.Url;

namespace Browsely.Modules.Dispatcher.Application.Urls;

internal sealed class GetContentQueryHandler(IUrlRepository urlRepository) : IQueryHandler<GetContentQuery, GetContentResponse>
{
    private readonly IUrlRepository _urlRepository = urlRepository ?? throw new ArgumentNullException(nameof(urlRepository));

    public async Task<Result<GetContentResponse>> Handle(GetContentQuery query, CancellationToken cancellationToken)
    {
        Url? url = await _urlRepository.GetAsync(query.Id, false, cancellationToken);
        if (url == null)
        {
            return Result.Failure<GetContentResponse>(UrlErrors.NotExists(query.Id));
        }

        var response = new GetContentResponse(
            query.Id,
            url.Uri,
            url.CurrentState?.ToString(),
            url.HtmlContent?.ToString(),
            url.CreatedOnUtc,
            url.ModifiedOnUtc
        );

        return Result.Success(response);
    }
}
