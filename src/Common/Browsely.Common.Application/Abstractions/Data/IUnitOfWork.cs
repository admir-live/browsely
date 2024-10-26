namespace Browsely.Common.Application.Abstractions.Data;

/// <summary>
///     Represents a unit of work that can save changes asynchronously.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Saves all changes made in this unit of work asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    ///     A task that represents the asynchronous save operation. The task result contains the number of state entries
    ///     written to the database.
    /// </returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
