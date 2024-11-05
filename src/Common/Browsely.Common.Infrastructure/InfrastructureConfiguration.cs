using Browsely.Common.Application.Clock;
using Browsely.Common.Application.Messaging;
using Browsely.Common.Infrastructure.Clock;
using Browsely.Common.Infrastructure.Messaging;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Browsely.Common.Infrastructure;

public record InfrastructureBusOptions(string Host, string VirtualHost, string Username, string Password);

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructures(
        this IServiceCollection services,
        InfrastructureBusOptions busOptions,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
        Action<IRabbitMqBusFactoryConfigurator, IBusRegistrationContext>? configureEndpoints = null)
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

                configureEndpoints?.Invoke(configurator, context);
                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
