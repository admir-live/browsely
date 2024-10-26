using Browsely.Common.Application.Exceptions;

namespace Browsely.Modules.Dispatcher.Domain.Url;

public sealed class ExpiredState : IUrlState
{
    public IUrlState Next()
    {
        throw new BrowselyException("Cannot transition to next state from Expired state.");
    }

    public override string ToString()
    {
        return "Expired";
    }
}
