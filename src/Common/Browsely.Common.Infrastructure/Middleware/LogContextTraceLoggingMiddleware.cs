using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Browsely.Common.Infrastructure.Middleware;

internal sealed class LogContextTraceLoggingMiddleware(RequestDelegate next)
{
    public Task Invoke(HttpContext context)
    {
        string traceId = Activity.Current?.TraceId.ToString();

        using (LogContext.PushProperty("TraceId", traceId))
        {
            return next.Invoke(context);
        }
    }
}
