using Browsely.Common.Application.Messaging;
using Browsely.Common.Domain;
using Browsely.Common.Presentation.Endpoints;
using Browsely.Modules.Dispatcher.Application.Urls;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Browsely.Modules.Dispatcher.Presentation.Urls;

internal sealed class ReviewUrl : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("url/review", async (Request request, IMessageBroker broker) =>
            {
                Result<Ulid> result = await broker.SendAsync(new ReviewUrlCommand(new Uri(request.Uri)));
                return result.IsSuccess ? Results.Accepted(request.Uri, result.Value) : Results.BadRequest(result.Error);
            })
            .WithTags(Tags.Urls);
    }

    private sealed record Request(string Uri);
}
