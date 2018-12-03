using FluentAssertions;
using NUnit.Framework;
using System;
using TestStack.BDDfy;

namespace Sammak.SandBox.Tests.Scenarios
{
    [Story(
        AsA = "Sammak SandBox App user",
        IWant = "to be able to perform Exception",
        SoThat = "I can verify the Exception funtions work"
    )]
    [TestFixture]
    public class ExceptionTestSpec
    {

        #region Tests

        [TestCase]
        //[Category(Constants.TestCatergory.VALIDATION)]
        public void ExceptionTest()
        {
            new ExceptionTestScenario().BDDfy();
        }

        #endregion Tests

        #region Scenarios
        internal class ExceptionTestScenario
        {
            private Action action;

            public ExceptionTestScenario()
            {
                //Startup.EnsureIocSetUp();
            }

            public void GivenSetupIsCorrect()
            {
            }

            public void WhenActionThrowsException()
            {
                action = () => throw new ExecutionEngineException();
            }

            public void ThenTheActionShouldThrowException()
            {
                action
                    .Should()
                    .Throw<ExecutionEngineException>();
            }
        }

        #endregion
    }
}
