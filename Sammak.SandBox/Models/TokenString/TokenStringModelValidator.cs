using FluentValidation;
using System.Linq;

namespace Sammak.SandBox.Models.TokenString
{
    public class TokenStringModelValidator : AbstractValidator<TokenStringModel>
    {
        public TokenStringModelValidator()
        {
            RuleFor(s => s).NotEmpty()
                .WithMessage("The Token string is missing or null or empty");
            RuleFor(s => s)
                .Must(BeValidUserIdentity)
                .WithMessage("The Token string must contain two '.' characters");
        }

        private bool BeValidUserIdentity(TokenStringModel model)
        {
            // the userIdentity must be of <domain>\username or username@<domain>.com email format
            string str = (string)model;
            var ret = model.TokenString.Count(x => x == '.') == 2;

            return ret;
        }
    }

}
