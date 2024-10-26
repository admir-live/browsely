using Microsoft.AspNetCore.Routing;

namespace Browsely.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
