using Browsely.Common.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Browsely.Common.Infrastructure.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseLogContext(this IApplicationBuilder app)
    {
        app.UseMiddleware<LogContextTraceLoggingMiddleware>();
        return app;
    }
}
