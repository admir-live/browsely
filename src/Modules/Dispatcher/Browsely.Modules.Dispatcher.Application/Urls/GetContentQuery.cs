using Browsely.Common.Application.Messaging;

namespace Browsely.Modules.Dispatcher.Application.Urls;

public sealed record GetContentQuery(Ulid Id) : IQuery<GetContentResponse>;
