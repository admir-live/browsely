using Browsely.Common.Application.Messaging;

namespace Browsely.Modules.Dispatcher.Application.Urls;

public sealed record ReviewUrlCommand(Uri Uri) : ICommand<Ulid>;
