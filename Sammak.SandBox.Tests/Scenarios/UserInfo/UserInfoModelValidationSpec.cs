using NUnit.Framework;
using Sammak.SandBox.Models.UserIdentity;
using Sammak.SandBox.Models.UserInfo;
using TestStack.BDDfy;
using FluentValidation.Results;
using FluentAssertions;

namespace Sammak.SandBox.Tests.Scenarios.UserInfo
{
    [Story(
        AsA = "Sammak SandBox App user",
        IWant = "to be able to perform userInfo Validation",
        SoThat = "I can verify the UserInfoValidation funtions work"
    )]
    [TestFixture]
    public class UserInfoModelValidationSpec
    {

        #region Tests

        [TestCase]
        //[Category(Constants.TestCatergory.VALIDATION)]
        public void UserInfoModelValidationTest()
        {
            new UserInfoModelValidation().BDDfy();
        }

        #endregion Tests

        #region Scenarios
        internal class UserInfoModelValidation
        {
            private UserInfoModel userInfo;
            private ValidationResult validationResult;

            public UserInfoModelValidation()
            {
                Startup.EnsureIocSetUp();
            }

            public void GivenValidUserInfoObject()
            {
                var userIdentityString = @"!def!xyz%abc@example.com";
                //var userIdentityString = @"""test""blah""@example.com";
                //var userIdentityString = "myname@.com";
                userInfo = new UserInfoModel(userIdentityString);

            }

            public void WhenUserInfoModelIsValidated()
            {
                validationResult = userInfo.Validate();
            }

            public void ThenTheValidationShouldHavePassed()
            {
                validationResult.IsValid.Should().BeTrue();
                validationResult.Errors.Should().BeEmpty();
            }
        }

        #endregion
    }
}
