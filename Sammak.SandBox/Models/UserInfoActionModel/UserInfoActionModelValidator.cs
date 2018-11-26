using FluentValidation;
using System.Text.RegularExpressions;

namespace Sammak.SandBox.Models.UserInfoAction
{
    public class UserInfoActionModelValidator : AbstractValidator<UserInfoActionModel>
    {
        public UserInfoActionModelValidator()
        {
            RuleFor(user => user.UserName).NotEmpty()
                .WithMessage("The user's Username cannot be empty");

            // option 1 for using "When"
            //RuleFor(user => user.UserName)
            //    .Must(userName => MeetUsernameConstraints(userName))
            //    .When(user => !string.IsNullOrWhiteSpace(user.UserName))
            //    .WithMessage("Username does not meet criteria");

            // option 2 for using "When"
            When((user => !string.IsNullOrWhiteSpace(user.UserName)), () =>
            {
                RuleFor(user => user.UserName)
                    .Must(userName => MeetUsernameConstraints(userName))
                    .WithMessage("Username does not meet criteria");
            });

            RuleFor(user => user.Name).NotEmpty()
                .WithMessage("The user's Name part cannot be empty");

            RuleFor(user => user.Domain).NotEmpty()
                .WithMessage("The User's Domain part cannot be empty");

            RuleFor(user => user.Email)
                .EmailAddress()
                .WithMessage("The User's Email Address is invalid");
        }

        private bool MeetUsernameConstraints(string username)
        {
            // All the following Regex should configurable in terms of length and allowed chars
            var hasMinMaxChars = new Regex(@"^.{8,15}$");
            var hasRestrictedSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            return hasMinMaxChars.IsMatch(username)
                   && !hasRestrictedSymbols.IsMatch(username);
        }

    }
}
