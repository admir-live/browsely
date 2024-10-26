namespace Browsely.Modules.Dispatcher.Domain.Url;

/// <summary>
///     Represents the abstract base class for different states of a URL.
/// </summary>
public interface IUrlState
{
    /// <summary>
    ///     Transitions to the next state of the URL.
    /// </summary>
    /// <returns>The next state of the URL.</returns>
    IUrlState Next();
}
