using FluentValidation;

namespace Browsely.Modules.Dispatcher.Application.Urls;

internal sealed class GetContentQueryValidator : AbstractValidator<GetContentQuery>
{
    public GetContentQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
