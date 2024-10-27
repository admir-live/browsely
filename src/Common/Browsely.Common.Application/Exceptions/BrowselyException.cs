using Browsely.Common.Domain;

namespace Browsely.Common.Application.Exceptions;

public sealed class BrowselyException(string message, Error? error = default, Exception? innerException = default) :
    Exception(message, innerException)
{
    public Error? Error { get; } = error;
}
