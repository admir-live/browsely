using Browsely.Common.Domain;

namespace Browsely.Modules.Dispatcher.Domain.Url;

public static class UrlErrors
{
    public static Error ReviewUrlFailed(Uri uri)
    {
        return Error.NotFound("Url.ReviewUrlFailed", $"There was an error processing the URL: {uri}");
    }
}
