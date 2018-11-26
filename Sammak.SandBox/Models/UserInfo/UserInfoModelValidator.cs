using FluentValidation;

namespace Sammak.SandBox.Models.UserInfo
{
    public class UserInfoModelValidator : AbstractValidator<UserInfoModel>
    {
        public UserInfoModelValidator()
        {
            RuleFor(user => user.UserIdentity).NotEmpty()
                .WithMessage("The User's UserIdentity cannot be empty");
            RuleFor(user => user.UserName).NotEmpty()
                .WithMessage("The User's UserName part cannot be empty");
            RuleFor(user => user.Domain).NotEmpty()
                .WithMessage("The User's Domain part cannot be empty");

            // the email address is ok to be empty and this rule passes if it is not present.  
            // if present, then it must comply with email rule
            RuleFor(user => user.EmailAddress)
                .EmailAddress()
                .WithMessage("The User's Email Address is invalid");
        }
    }
}
