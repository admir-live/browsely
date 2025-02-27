﻿namespace Browsely.Modules.Dispatcher.Domain.Url;

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
    /// <param name="withTracking">A flag indicating whether to track the entity.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the URL.</returns>
    Task<Url?> GetAsync(Ulid id, bool withTracking = false, CancellationToken cancellationToken = default);


    /// <summary>
    ///     Retrieves a URL from the repository by its URL asynchronously.
    /// </summary>
    /// <param name="url">The URL to retrieve.</param>
    /// <param name="withTracking">A flag indicating whether to track the entity.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the URL.</returns>
    Task<Url?> GetByUrlAsync(Uri url, bool withTracking = false, CancellationToken cancellationToken = default);
}
