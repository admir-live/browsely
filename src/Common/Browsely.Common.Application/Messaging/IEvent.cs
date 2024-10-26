namespace Browsely.Common.Application.Messaging;

/// <summary>
///     Represents an event with a unique identifier and a timestamp indicating when it occurred.
/// </summary>
public interface IEvent
{
    /// <summary>
    ///     Gets the unique identifier of the event.
    /// </summary>
    Ulid Id { get; }

    /// <summary>
    ///     Gets the UTC timestamp of when the event occurred.
    /// </summary>
    DateTime OccurredOnUtc { get; }
}
