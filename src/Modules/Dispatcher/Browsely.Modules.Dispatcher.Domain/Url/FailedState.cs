using Browsely.Common.Application.Exceptions;

namespace Browsely.Modules.Dispatcher.Domain.Url;

public sealed class FailedState : IUrlState
{
    public IUrlState Next()
    {
        throw new BrowselyException("Cannot transition to next state from Failed state.");
    }

    public override string ToString()
    {
        return "Failed";
    }
}
