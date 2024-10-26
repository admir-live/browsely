using Browsely.Common.Application.Clock;
using Browsely.Common.Application.Messaging;
using BrowselyCommon.Infrastructure.Clock;
using BrowselyCommon.Infrastructure.Messaging;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BrowselyCommon.Infrastructure;

public record InfrastructureBusOptions(string Host, string VirtualHost, string Username, string Password);

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        InfrastructureBusOptions busOptions,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers)
    {
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.TryAddSingleton<IEventBus, DefaultMessageBroker>();
        services.TryAddSingleton<IMessageBroker, DefaultMessageBroker>();

        services.AddMassTransit(configure =>
        {
            foreach (Action<IRegistrationConfigurator> configureConsumers in moduleConfigureConsumers)
            {
                configureConsumers(configure);
            }

            configure.SetKebabCaseEndpointNameFormatter();
            configure.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(busOptions.Host, busOptions.VirtualHost, h =>
                {
                    h.Username(busOptions.Username);
                    h.Password(busOptions.Password);
                });

                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
