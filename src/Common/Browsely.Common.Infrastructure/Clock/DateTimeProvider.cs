using Browsely.Common.Application.Clock;

namespace BrowselyCommon.Infrastructure.Clock;

/// <inheritdoc />
internal sealed class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}
