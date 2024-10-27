namespace Browsely.Modules.Dispatcher.Domain.Url;

/// <summary>
///     Represents a repository for managing URL entities.
/// </summary>
public interface IUrlRepository
{
    /// <summary>
    ///     Adds a new URL to the repository.
    /// </summary>
    /// <param name="url">The URL to add.</param>
    void Add(Url url);

    /// <summary>
    ///     Updates an existing URL in the repository.
    /// </summary>
    /// <param name="url">The URL to update.</param>
    void Update(Url url);

    /// <summary>
    ///     Retrieves a URL from the repository asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the URL.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the URL.</returns>
    Task<Url?> GetAsync(Ulid id, CancellationToken cancellationToken = default);
}
