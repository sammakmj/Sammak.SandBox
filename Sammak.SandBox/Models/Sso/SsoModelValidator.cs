using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace Sammak.SandBox.Models.Sso
{
    public class SsoModelValidator : AbstractValidator<SsoModel>
    {
        public SsoModelValidator()
        {
            RuleFor(model => model.NickName).NotEmpty()
                .WithMessage("The user's Nickname cannot be empty");

            // this following validation rule runs only when the first rule passes.  
            // if the model name is empty, the first rule already indicates the problem and it makes no sense to say the username criteria is not met.
            RuleFor(model => model.NickName)
                .Must(nickName => MeetUserNameConstraints(nickName))
                .When(model => !string.IsNullOrWhiteSpace(model.NickName))
                .WithMessage("Nickname does not meet criteria");

            RuleFor(model => model.UserName).NotEmpty()
                .WithMessage("The user's Username cannot be empty");

            RuleFor(model => model.UserName)
                .EmailAddress()
                .When(model => !string.IsNullOrWhiteSpace(model.UserName))
                .WithMessage("Username must conform to email address criteria");

            RuleFor(model => model.Domain).NotEmpty()
                .WithMessage("The User's Domain cannot be empty");

            RuleFor(model => model.Email)
                .EmailAddress()
                .When(model => !string.IsNullOrWhiteSpace(model.Email))
                .WithMessage("The User's Email Address is invalid");

            RuleFor(model => model.AppId).NotEmpty()
                .WithMessage("The AppId property is missing or null or empty");

            RuleFor(model => model.AppId)
                .Must(appId => Guid.TryParse(appId, out Guid discard))
                .When(model => !string.IsNullOrWhiteSpace(model.AppId))
                .WithMessage("The AppId property must be a valid Guid");
        }

        private bool MeetUserNameConstraints(string username)
        {
            // All the following Regex should configurable in terms of length and allowed chars
            var hasMinMaxChars = new Regex(@"^.{8,15}$");
            var hasRestrictedSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            return hasMinMaxChars.IsMatch(username)
                   && !hasRestrictedSymbols.IsMatch(username);
        }

    }
}
