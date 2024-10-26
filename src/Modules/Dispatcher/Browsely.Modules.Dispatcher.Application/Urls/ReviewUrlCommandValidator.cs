using FluentValidation;

namespace Browsely.Modules.Dispatcher.Application.Urls;

internal sealed class ReviewUrlCommandValidator : AbstractValidator<ReviewUrlCommand>
{
    public ReviewUrlCommandValidator()
    {
        RuleFor(c => c.Uri)
            .NotEmpty().WithMessage("The Uri must not be empty.")
            .Must(BeAValidUri).WithMessage("The Uri must be a valid URL."); // Potential improvement: use a custom validator to check URI based on compiled regex.
    }

    private static bool BeAValidUri(string uri)
    {
        return Uri.TryCreate(uri, UriKind.Absolute, out Uri? _);
    }
}
