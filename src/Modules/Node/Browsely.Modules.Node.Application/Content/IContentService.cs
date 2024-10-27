namespace Browsely.Modules.Node.Application.Content;

/// <summary>
///     Defines the contract for a service that retrieves content from a given URL.
/// </summary>
public interface IContentService
{
    /// <summary>
    ///     Asynchronously retrieves content from the specified URL.
    /// </summary>
    /// <param name="request">The URL from which to retrieve the content.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the content.</returns>
    Task<ContentResponse> GetContentAsync(ContentRequest request, CancellationToken cancellationToken = default);
}
