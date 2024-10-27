using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Browsely.Common.Infrastructure.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Browsely API",
                Version = "v1",
                Description = "Browsely API for TRG."
            });

            options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
        });

        return services;
    }
}
