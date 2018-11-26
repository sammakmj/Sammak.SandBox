namespace Sammak.SandBox.Models.Requests
{
    /// <summary>
    /// Holds the Single Sign On credentials to validate
    /// </summary>
    public class SsoRequest : ActionRequestBase
    {
        /// <summary>
        /// The Token to validate
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The Application Id to get access via SSO
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void NormalizeRequest()
        {
            Token = Token?.Trim();
        }

    }
}