namespace Browsely.Common.Application.Clock;

/// <summary>
///     Provides an interface for accessing the current date and time.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    ///     Gets the current date and time in UTC.
    /// </summary>
    public DateTime UtcNow { get; }
}
