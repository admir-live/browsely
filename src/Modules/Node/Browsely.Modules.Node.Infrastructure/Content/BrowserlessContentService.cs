using System.Net;
using System.Net.Http.Json;
using Browsely.Modules.Node.Application.Content;
using Microsoft.Extensions.Logging;

namespace Browsely.Modules.Node.Infrastructure.Content;

public sealed class BrowserlessContentService(HttpClient httpClient, ILogger<BrowserlessContentService> logger) : IContentService
{
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private readonly ILogger<BrowserlessContentService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<ContentResponse> GetContentAsync(ContentRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("content", request, cancellationToken);
            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            LogBasedOnStatusCode(response.StatusCode, request.Url);
            return new ContentResponse(response.StatusCode, content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while attempting to retrieve content from URL '{Url}'", request.Url);
            return new ContentResponse(HttpStatusCode.InternalServerError, string.Empty);
        }
    }

    private void LogBasedOnStatusCode(HttpStatusCode statusCode, Uri url)
    {
        string logMessage = statusCode switch
        {
            HttpStatusCode.OK => $"Successfully retrieved content from URL '{{Url}}'. Status code: {(int)statusCode}",
            HttpStatusCode.NotFound => $"Content not found at URL '{{Url}}'. Status code: {(int)statusCode}",
            HttpStatusCode.InternalServerError => $"Server error when accessing URL '{{Url}}'. Status code: {(int)statusCode}",
            _ => $"Failed to retrieve content from URL '{{Url}}'. Status code: {(int)statusCode}"
        };

        LogLevel logLevel = statusCode switch
        {
            HttpStatusCode.OK => LogLevel.Information,
            HttpStatusCode.NotFound => LogLevel.Warning,
            HttpStatusCode.InternalServerError => LogLevel.Error,
            _ => LogLevel.Error
        };

        _logger.Log(logLevel, logMessage, url);
    }
}
