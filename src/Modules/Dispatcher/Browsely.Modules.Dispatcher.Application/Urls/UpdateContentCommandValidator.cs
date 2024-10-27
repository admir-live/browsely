using FluentValidation;

namespace Browsely.Modules.Dispatcher.Application.Urls;

internal sealed class UpdateContentCommandValidator : AbstractValidator<UpdateContentCommand>
{
    public UpdateContentCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.StatusCode).NotEmpty();
        RuleFor(x => x.Content).NotEmpty();
    }
}
