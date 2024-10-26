using Browsely.Common.Application.Messaging;
using Browsely.Common.Domain;
using Browsely.Common.Presentation.Endpoints;
using Browsely.Common.Presentation.Results;
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
                Result<Ulid> result = await broker.SendAsync(new ReviewUrlCommand(request.Uri));
                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Urls);
    }

    private sealed record Request(string Uri);
}
