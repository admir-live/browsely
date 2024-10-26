using Browsely.Common.Domain;

namespace Browsely.Common.Application.Exceptions;

public sealed class BrowselyException(string requestName, Error? error = default, Exception? innerException = default) :
    Exception("Application exception", innerException)
{
    public string RequestName { get; } = requestName;
    public Error? Error { get; } = error;
}
