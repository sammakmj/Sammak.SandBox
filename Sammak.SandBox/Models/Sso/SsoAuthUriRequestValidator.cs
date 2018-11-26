using FluentValidation;
using Sammak.SandBox.Helpers;

namespace Sammak.SandBox.Models.Sso
{
    public class SsoAuthUriRequestValidator : AbstractValidator<SsoAuthUriRequest>
    {
        public SsoAuthUriRequestValidator()
        {
            RuleFor(model => model.CallBackUrl).NotEmpty()
                .WithMessage("The CallBackUrl cannot be empty");

            RuleFor(model => model.CallBackUrl)
                .Must(url => url.IsValidUrl())
                .When(model => !string.IsNullOrWhiteSpace(model.CallBackUrl))
                .WithMessage("URL does not meet criteria");

        }

    }
}
