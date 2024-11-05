using System.Net;
using Browsely.Common.Application.Abstractions.Data;
using Browsely.Common.Application.Extensions;
using Browsely.Common.Infrastructure;
using Browsely.Common.Presentation.Endpoints;
using Browsely.Modules.Dispatcher.Domain.Url;
using Browsely.Modules.Dispatcher.Infrastructure.Database;
using Browsely.Modules.Dispatcher.Infrastructure.Urls;
using Browsely.Modules.Dispatcher.Presentation;
using Browsely.Modules.Dispatcher.Presentation.Urls;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;

namespace Browsely.Modules.Dispatcher.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureMessageBroker(configuration);
        services.ConfigureDatabase(configuration);
        services.ConfigureEndpoints();
        services.ConfigureRepositories();

        return services;
    }

    public static IServiceCollection AddHttpClientWithRetryPolicy<TClient, TImplementation>(
        this IServiceCollection services,
        string clientKey,
        Uri baseUrl,
        int retryCount = 3)
        where TClient : class
        where TImplementation : class, TClient
    {
        services.AddHttpClient<TClient, TImplementation>(clientKey, (_, client) =>
            {
                client.BaseAddress = baseUrl;
            })
            .AddPolicyHandler((provider, _) =>
            {
                ILogger<TImplementation> logger = provider.GetRequiredService<ILogger<TImplementation>>();
                return GetRetryPolicy(logger, retryCount);
            });

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy<TImplementation>(ILogger<TImplementation> logger, int retryCount)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (outcome, timespan, retryAttempt, _) =>
                {
                    logger.LogWarning("Request failed with {StatusCode}. Waiting {WaitTime} before next retry. Retry attempt {RetryAttempt}.",
                        outcome.Result?.StatusCode, timespan, retryAttempt);
                });
    }

    private static void ConfigureMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        InfrastructureBusOptions busSettings = GetBusSettings(configuration);
        services.TryAddSingleton(busSettings);
        services.AddInfrastructures(busSettings, [ConfigureConsumers]);
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

    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, DispatcherDbContextUnitOfWork>();
        services.AddScoped<IUrlRepository, UrlRepository>();
    }

    private static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<OnUrlReviewedEventHandler>();
    }
}
