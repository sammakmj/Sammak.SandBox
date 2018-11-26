using Sammak.SandBox.Helpers;
using System.ComponentModel;

namespace Sammak.SandBox.Models.Sso
{
    /// <summary>
    /// 
    /// </summary>
    public class SsoActionResult : ActionResultBase
    {
        /// <summary>
        /// Cast <see cref="ResultBase.ResultType"/> to <see cref="SsoResultType"/> for easier reading of the result
        /// </summary>
        public enum SsoResultType
        {
            /// <summary>
            /// 
            /// </summary>
            [Description("The operation is successful")]
            Success,

            /// <summary>
            /// 
            /// </summary>
            [Description("The Single-Sign-On action has failed")]
            Failed,  // This case will cover any other case different from the following list

            /// <summary>
            /// 
            /// </summary>
            [Description("The Token Validate Audience Failure")]
            ValidateAudienceFail,

            /// <summary>
            /// 
            /// </summary>
            [Description("Invalid Input Argument")]
            ValidationFailure,

            /// <summary>
            /// 
            /// </summary>
            [Description("Token does not meet criteria")]
            InvalidToken,

            /// <summary>
            /// 
            /// </summary>
            [Description("Invalid Domain")]
            InvalidDomain,

            /// <summary>
            /// 
            /// </summary>
            [Description("No Active Practice exists for the requested Domain")]
            NoActivePracticeForDomain,

            /// <summary>
            /// 
            /// </summary>
            [Description("Single Sign On is not turned on for the requested Domain")]
            SsoIsOff,

            /// <summary>
            /// 
            /// </summary>
            [Description("No SSO Feature exists for the requested Domain")]
            NoSsoFeatureForDomain,

            /// <summary>
            /// 
            /// </summary>
            [Description("The requested Application Id is not configured for SSO")]
            NoSsoForAppId,

            /// <summary>
            /// 
            /// </summary>
            [Description("Token has invalid signature")]
            TokenInvalidSignature,

            /// <summary>
            /// 
            /// </summary>
            [Description("Token has expired")]
            TokenExpired,

            /// <summary>
            /// 
            /// </summary>
            [Description("The Application Id is not valid")]
            InvalidAppId,

            /// <summary>
            /// 
            /// </summary>
            [Description("The User is not found")]
            UserNotFound,

        }

        /// <summary>
        /// Returns textual description of the result enumeration value of the action result on SSO request
        /// </summary>
        /// <see cref="SsoActionResult"/>
        public override string ResultTypeDescription { get => ((SsoResultType)ResultType).GetDescription(); }

        /// <summary>
        /// if a succesfull result is obtained, the username should be returned
        /// </summary>
        public string UserName { get; set; }

    }
}
