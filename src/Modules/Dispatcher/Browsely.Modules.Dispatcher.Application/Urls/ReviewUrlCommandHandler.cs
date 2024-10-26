using Browsely.Common.Application.Messaging;
using Browsely.Common.Domain;

namespace Browsely.Modules.Dispatcher.Application.Urls;

internal sealed class ReviewUrlCommandHandler : ICommandHandler<ReviewUrlCommand, Ulid>
{
    public Task<Result<Ulid>> Handle(ReviewUrlCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result.Success(Ulid.NewUlid()));
    }
}
