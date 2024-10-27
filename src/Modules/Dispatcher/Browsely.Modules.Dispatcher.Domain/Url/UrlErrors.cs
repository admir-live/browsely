using Browsely.Common.Domain;

namespace Browsely.Modules.Dispatcher.Domain.Url;

public static class UrlErrors
{
    public static Error ReviewUrlFailed(Uri uri)
    {
        return Error.Failure("Url.ReviewUrlFailed", $"There was an error processing the URL: {uri}");
    }

    public static Error NotExists(Ulid id)
    {
        return Error.NotFound("Url.NotExists", $"The URL with ID {id} does not exist.");
    }

    public static Error InProcessingState(Uri uri)
    {
        return Error.Conflict("Url.InProcessingState", $"The URL {uri} is already in the processing state.");
    }
}
