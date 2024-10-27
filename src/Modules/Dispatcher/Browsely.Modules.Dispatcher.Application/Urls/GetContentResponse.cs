using Browsely.Modules.Dispatcher.Domain.Url;

namespace Browsely.Modules.Dispatcher.Application.Urls;

public sealed record GetContentResponse(
    Ulid Id,
    Uri Url,
    string? State,
    string? Content,
    DateTime CreatedAt,
    DateTime UpdatedAt);
