using FluentValidation;

namespace Sammak.SandBox.Models.UserIdentity
{
    public class UserIdentityModelValidator : AbstractValidator<UserIdentityModel>
    {
        public UserIdentityModelValidator()
        {
            RuleFor(model => model)
                .Must(BeValidUserIdentity)
                .WithMessage("The UserIdentity must contain either @ or \\ character");
        }

        private bool BeValidUserIdentity(UserIdentityModel userIdentity)
        {
            // the userIdentity must be of <domain>\username or username@<domain>.com email format
            var identity = (string)userIdentity; // explicit cast
            return identity.Contains("@") || identity.Contains("\\");
        }
    }
}
