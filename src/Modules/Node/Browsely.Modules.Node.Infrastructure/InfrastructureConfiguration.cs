using Browsely.Common.Application.Extensions;
using Browsely.Modules.Dispatcher.Infrastructure;
using Browsely.Modules.Node.Application.Content;
using Browsely.Modules.Node.Infrastructure.Content;
using Browsely.Modules.Node.Presentation.Urls;
using BrowselyCommon.Infrastructure;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Browsely.Modules.Node.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureMessageBroker(configuration);
        services.ConfigureContentClient(configuration);
        return services;
    }

    private static void ConfigureContentClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClientWithRetryPolicy<IContentService, BrowserlessContentService>(
            "BrowserlessClient",
            configuration.GetValueOrThrow<Uri>("Browserless:BaseUrl")
        );
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

    private static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<OnUrlReviewScheduledEventHandler>();
    }
}
