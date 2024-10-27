using System.Net;

namespace Browsely.Modules.Node.Application.Content;

public record ContentResponse(HttpStatusCode StatusCode, string Content);
