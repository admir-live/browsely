using FluentValidation;

namespace Browsely.Modules.Dispatcher.Application.Urls;

internal sealed class ReviewUrlCommandValidator : AbstractValidator<ReviewUrlCommand>
{
    public ReviewUrlCommandValidator()
    {
        RuleFor(c => c.Uri)
            .NotEmpty().WithMessage("The Uri must not be empty.");
    }
}
