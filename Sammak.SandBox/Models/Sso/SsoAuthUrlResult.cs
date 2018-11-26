using Sammak.SandBox.Helpers;
using System;
using System.ComponentModel;

namespace Sammak.SandBox.Models.Sso
{
    public class SsoAuthUrlResult : ActionResultBase
    {
        /// <summary>
        /// Cast <see cref="ActionResultBase.ResultType"/> to <see cref="SsoAuthUrlResultType"/> for easier reading of the result
        /// </summary>
        public enum SsoAuthUrlResultType
        {
            /// <summary>
            /// 
            /// </summary>
            [Description("The operation is successful")]
            Success,

            /// <summary>
            /// 
            /// </summary>
            [Description("The Build Auth0 Url funcion has failed")]
            Failed,  // This case will cover any other case different from the following list

            /// <summary>
            /// The input payload object has some invalid property. See the result description text
            /// </summary>
            [Description("Invalid Input Argument")]
            ValidationFailure,

        }

        /// <summary>
        /// Returns textual description of the result enumeration value of the action result on this request
        /// </summary>
        /// <see cref="SsoActionResult"/>
        public override string ResultTypeDescription { get { return ((SsoAuthUrlResultType)ResultType).GetDescription(); } }

        /// <summary>
        /// Upon successful operation this returned property contains the Auth0 Url
        /// </summary>
        public Uri Uri { get; set; }
    }
}
