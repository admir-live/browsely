using Browsely.Common.Application.Messaging;
using Browsely.Common.Domain;
using Browsely.Common.Presentation.Endpoints;
using Browsely.Common.Presentation.Results;
using Browsely.Modules.Dispatcher.Application.Urls;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Browsely.Modules.Dispatcher.Presentation.Urls;

internal sealed class GetUrl : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("url/{id}", async (Ulid id, IMessageBroker broker) =>
            {
                Result<GetContentResponse> result = await broker.SendAsync(new GetContentQuery(id));
                return result.Match(Results.Ok, Results.NotFound);
            })
            .WithTags(Tags.Urls);
    }
}
