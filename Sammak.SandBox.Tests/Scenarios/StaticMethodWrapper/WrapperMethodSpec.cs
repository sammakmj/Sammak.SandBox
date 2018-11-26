using NUnit.Framework;
using Sammak.SandBox.Models.UserIdentity;
using Sammak.SandBox.Models.UserInfo;
using TestStack.BDDfy;
using FluentValidation.Results;
using FluentAssertions;
using Sammak.SandBox.InterfaceExtension.SomeLibraryYouCanModify;

namespace Sammak.SandBox.Tests.Scenarios.StaticMethodWrapper
{
    [Story(
        AsA = "Sammak SandBox App user",
        IWant = "to be able to perform Static Method Wrapper Validation",
        SoThat = "I can verify the Static Method Wrapper funtions work"
    )]
    [TestFixture]
    public class WrapperMethodTestsSpec
    {

        private class TestWrapper : IStaticWrapper
        {
            public bool SomeStaticMethod(string input)
            {
                return !string.IsNullOrEmpty(input);
            }
        }

        #region Tests

        [TestCase]
        public void SomeMethod_GivenNull_ShouldReturnZero()
        {
            var wrapper = new TestWrapper();

            var wm = new WrapperMethod(wrapper);

            var output = wm.SomeMethod(null);

            Assert.AreEqual(0, output);
        }

        #endregion
    }
}
