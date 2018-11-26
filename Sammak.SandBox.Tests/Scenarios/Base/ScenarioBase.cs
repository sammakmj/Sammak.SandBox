using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sammak.SandBox.Tests.Scenarios.Base
{
    public abstract class ScenarioBase
    {
    }
//    public abstract class EsaBaseScenario : BaseServiceScenario
//    {
//        #region Scenario class Properties

//        protected Mock<ILogger> MockLogger { get; set; }

//        protected string MockUserName { get; set; }

//        protected string MockExamCode { get; set; }

//        protected string MockAdminDate { get; set; }

//        protected string MockExamSeriesCode { get; set; }

//        protected ValidationResult ValidationResult { get; set; }

//        #endregion

//        #region ctor

//        protected EsaBaseScenario() : base()
//        {
//#if DEBUG
//            LogTest.OutputLogs = true;
//#endif
//        }

//        #endregion

//        #region Initialize, Setup and TearDown

//        protected override List<Type> AdditionalDependencies()
//        {
//            // list of dependencies
//            var types = base.AdditionalDependencies();
//            types.Add(typeof(IBusControl));
//            types.Add(typeof(IValidationFactory));
//            types.Add(typeof(IEsaInterService));
//            return types;
//        }

//        /// <summary>
//        /// Anything before the main setup. Abstract.
//        /// </summary>
//        protected override void PreSetup()
//        {
//            MockLogger = new Mock<ILogger>();
//            MockUserName = Utilities.Constants.UserName.ESA_TesterUserName;
//            MockExamCode = ExamCodeBuilder.BuildAlphaNumeric(Random);
//            MockAdminDate = AdminDateBuilder.Build(Random);
//            MockExamSeriesCode = ExamSeriesCodeBuilder.BuildNumeric(Random);
//        }

//        /// <summary>
//        /// Anything after the main setup. Abstract. Typically for anything that requires the Container to be ready
//        /// </summary>
//        protected override void PostSetup()
//        {
//        }

//        #endregion

//        #region Common Then verifying methods

//        protected void NoExceptionShouldHaveBeenThrown()
//        {
//            DebuggingExceptionHandler.ConsoleWriteLineExceptionDetail(ExceptionCaught);
//            ExceptionCaught.Should().BeNull();
//        }

//        #endregion

//    }
}
