namespace Browsely.Modules.Node.Application.Docker;

/// <summary>
///     Represents a service for managing Docker containers.
/// </summary>
public interface IDockerService
{
    /// <summary>
    ///     Ensures that the specified Docker container is running.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task EnsureDockerContainerRunningAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Ensures that the specified Docker container is stopped.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task EnsureDockerContainerStoppedAsync(CancellationToken cancellationToken = default);
}
