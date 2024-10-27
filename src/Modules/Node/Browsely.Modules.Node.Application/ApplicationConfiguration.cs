using System.Reflection;
using Browsely.Common.Application;
using Browsely.Modules.Node.Application.Docker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Browsely.Modules.Node.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services, Assembly[] moduleAssemblies, IConfiguration configuration)
    {
        services.AddApplication(moduleAssemblies);

        services.Configure<DockerContainerConfig>(configuration.GetSection("Docker"));
        services.Configure<DockerInstanceConfig>(configuration.GetSection("Docker"));

        services.TryAddSingleton<IDockerService, DockerService>();
        return services;
    }
}
