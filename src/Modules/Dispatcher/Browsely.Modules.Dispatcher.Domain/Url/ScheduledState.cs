namespace Browsely.Modules.Dispatcher.Domain.Url;

public sealed class ScheduledState : IUrlState
{
    public IUrlState Next()
    {
        return new ActiveState();
    }

    public override string ToString()
    {
        return "Scheduled";
    }
}
