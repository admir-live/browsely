namespace Browsely.Modules.Dispatcher.Domain.Url;

public sealed class ActiveState : IUrlState
{
    public IUrlState Next()
    {
        return new ExpiredState();
    }

    public override string ToString()
    {
        return "Active";
    }
}
