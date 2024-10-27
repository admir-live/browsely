using System.Net;
using Browsely.Common.Application.Messaging;
using Browsely.Modules.Dispatcher.Domain.Url;

namespace Browsely.Modules.Dispatcher.Application.Urls;

public sealed record UpdateContentCommand(Ulid Id, HttpStatusCode StatusCode, Payload Content) : ICommand;
