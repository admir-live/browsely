using Browsely.Common.Application.Clock;

namespace Browsely.Common.Infrastructure.Clock;

/// <inheritdoc />
internal sealed class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}
