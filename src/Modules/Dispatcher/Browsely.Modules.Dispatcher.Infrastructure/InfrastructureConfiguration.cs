using System.Reflection;
using Browsely.Common.Application.Extensions;
using Browsely.Common.Presentation.Endpoints;
using Browsely.Modules.Dispatcher.Presentation;
using BrowselyCommon.Infrastructure;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Browsely.Modules.Dispatcher.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var busSettings = new InfrastructureBusOptions(
            configuration.GetRequiredValue("MessageBroker:Host"),
            configuration.GetRequiredValue("MessageBroker:VirtualHost"),
            configuration.GetRequiredValue("MessageBroker:Username"),
            configuration.GetRequiredValue("MessageBroker:Password")
        );

        services.TryAddSingleton(busSettings);
        services.AddInfrastructure(busSettings, [ConfigureConsumers]);

        services.AddEndpoints(AssemblyReference.Assembly);
        return services;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumers(Assembly.GetExecutingAssembly());
        // Need to add consumers here
    }
}
