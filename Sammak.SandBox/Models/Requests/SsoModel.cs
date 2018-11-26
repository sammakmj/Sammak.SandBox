using System;

namespace Sammak.SandBox.Models.Requests
{
    /// <summary>
    /// Holds the Single Sign On request model
    /// </summary>
    public class SsoModel
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEmoryUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSsoUser { get; set; }

        /// <summary>
        /// The Application Id to get access via SSO
        /// </summary>
        public string AppId { get; set; }

        #endregion

    }
}