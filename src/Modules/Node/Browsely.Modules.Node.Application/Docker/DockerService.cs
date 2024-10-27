﻿using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Browsely.Modules.Node.Application.Docker;

public sealed class DockerService(IOptions<DockerInstanceConfig> config, IOptions<DockerContainerConfig> containerConfig, ILogger<DockerService> logger) : IDockerService
{
    private const string RunningState = "running";
    private readonly DockerClient _client = new DockerClientConfiguration(config.Value.Host).CreateClient();
    private readonly DockerContainerConfig _containerConfig = containerConfig.Value ?? throw new ArgumentNullException(nameof(containerConfig));
    private readonly ILogger<DockerService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task EnsureDockerContainerRunningAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // Ensure any existing container with the same name is stopped and removed
            await EnsureDockerContainerStoppedAsync(cancellationToken);

            _logger.LogInformation("Pulling the latest version of the image '{ImageName}'...", _containerConfig.ImageName);
            await _client.Images.CreateImageAsync(new ImagesCreateParameters
            {
                FromImage = _containerConfig.ImageName,
                Tag = _containerConfig.Tag
            }, null, new Progress<JSONMessage>(), cancellationToken);

            _logger.LogInformation("Image '{ImageName}' has been pulled and is ready.", _containerConfig.ImageName);

            var createContainerParameters = new CreateContainerParameters
            {
                Image = _containerConfig.ImageName,
                Name = _containerConfig.ContainerName,
                HostConfig = new HostConfig
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {
                            $"{_containerConfig.ContainerPort}/tcp", new List<PortBinding>
                            {
                                new()
                                {
                                    HostPort = $"{_containerConfig.HostPort}"
                                }
                            }
                        }
                    }
                },
                ExposedPorts = new Dictionary<string, EmptyStruct>
                {
                    { $"{_containerConfig.ContainerPort}/tcp", new EmptyStruct() }
                }
            };

            CreateContainerResponse newContainer = await _client.Containers.CreateContainerAsync(createContainerParameters, cancellationToken);
            _logger.LogInformation("A new container '{ContainerName}' has been created.", _containerConfig.ContainerName);

            bool started = await _client.Containers.StartContainerAsync(newContainer.ID, new ContainerStartParameters(), cancellationToken);
            if (started)
            {
                _logger.LogInformation("Container '{ContainerName}' successfully started on port {HostPort}.", _containerConfig.ContainerName, _containerConfig.HostPort);
            }
            else
            {
                _logger.LogWarning("Container '{ContainerName}' could not be started.", _containerConfig.ContainerName);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while starting the Docker container '{ContainerName}'.", _containerConfig.ContainerName);
            throw;
        }
    }

    public async Task EnsureDockerContainerStoppedAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IList<ContainerListResponse> existingContainers = await _client.Containers.ListContainersAsync(new ContainersListParameters
            {
                All = true
            }, cancellationToken);

            ContainerListResponse existingContainer = existingContainers.FirstOrDefault(c => c.Names.Contains($"/{_containerConfig.ContainerName}"));

            if (existingContainer != null)
            {
                _logger.LogInformation("Container '{ContainerName}' already exists. Stopping and removing the existing container...", _containerConfig.ContainerName);

                if (existingContainer.State == RunningState)
                {
                    await _client.Containers.StopContainerAsync(existingContainer.ID, new ContainerStopParameters(), cancellationToken);
                    _logger.LogInformation("Container '{ContainerName}' has been stopped.", _containerConfig.ContainerName);
                }

                await _client.Containers.RemoveContainerAsync(existingContainer.ID, new ContainerRemoveParameters
                {
                    Force = true
                }, cancellationToken);

                _logger.LogInformation("The existing container '{ContainerName}' has been removed.", _containerConfig.ContainerName);
            }
            else
            {
                _logger.LogInformation("No existing container found with the name '{ContainerName}'.", _containerConfig.ContainerName);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while stopping and removing the Docker container '{ContainerName}'.", _containerConfig.ContainerName);
            throw;
        }
    }
}
