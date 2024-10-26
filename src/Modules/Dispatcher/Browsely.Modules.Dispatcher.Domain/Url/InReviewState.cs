namespace Browsely.Modules.Dispatcher.Domain.Url;

public sealed class InReviewState : IUrlState
{
    public IUrlState Next()
    {
        return new ActiveState();
    }

    public override string ToString()
    {
        return "In Review";
    }
}
