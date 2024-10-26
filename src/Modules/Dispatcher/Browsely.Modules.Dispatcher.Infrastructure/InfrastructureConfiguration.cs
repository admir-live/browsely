using System.Reflection;
using Browsely.Common.Application.Extensions;
using Browsely.Common.Presentation.Endpoints;
using Browsely.Modules.Dispatcher.Infrastructure.Database;
using Browsely.Modules.Dispatcher.Presentation;
using BrowselyCommon.Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Browsely.Modules.Dispatcher.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureMessageBroker(configuration);
        services.ConfigureDatabase(configuration);
        services.ConfigureEndpoints();

        return services;
    }

    private static void ConfigureMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        InfrastructureBusOptions busSettings = GetBusSettings(configuration);
        services.TryAddSingleton(busSettings);
        services.AddInfrastructure(busSettings, [ConfigureConsumers]);
    }

    private static InfrastructureBusOptions GetBusSettings(IConfiguration configuration)
    {
        return new InfrastructureBusOptions(
            configuration.GetValueOrThrow<string>("MessageBroker:Host"),
            configuration.GetValueOrThrow<string>("MessageBroker:VirtualHost"),
            configuration.GetValueOrThrow<string>("MessageBroker:Username"),
            configuration.GetValueOrThrow<string>("MessageBroker:Password")
        );
    }

    private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DispatcherDbContext>((_, builder) =>
        {
            builder.UseSqlServer(configuration.GetConnectionString("DispatcherDatabase"));
        });
    }

    private static void ConfigureEndpoints(this IServiceCollection services)
    {
        services.AddEndpoints(AssemblyReference.Assembly);
    }

    private static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumers(Assembly.GetExecutingAssembly());
        // Add additional consumer configurations here if needed
    }
}
