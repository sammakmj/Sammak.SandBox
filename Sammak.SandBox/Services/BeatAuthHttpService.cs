using Sammak.SandBox.Models.Requests;
using Sammak.SandBox.Models.Sso;
using System;
using System.Configuration;
using System.Threading.Tasks;
using SsoAuthUriRequest = Sammak.SandBox.Models.Requests.SsoAuthUriRequest;
using SsoModel = Sammak.SandBox.Models.Requests.SsoModel;

namespace Sammak.SandBox.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class BeatAuthHttpService : HttpService
    {
        #region Private variables

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public BeatAuthHttpService(): base(RootUrl())
        {
        }

        #endregion

        #region Static Initializer Methods

        private static string RootUrl()
        {
            var beatAuthHost = ConfigurationManager.AppSettings["Beat.Auth.Host"];
            var beatAuthPort = ConfigurationManager.AppSettings["Beat.Auth.Port"];
            var beatAuthApiPrefix = ConfigurationManager.AppSettings["Beat.Auth.ApiPrefix"];

            if (string.IsNullOrWhiteSpace(beatAuthHost) || beatAuthHost.Length < 1)
            {
                throw new Exception("The 'Beat.Auth.Host' URL define is missing from the config file!");
            }

            // NOTE: the 'Beat.Auth.Port' and/or 'Beat.Auth.ApiPrefix' defines optiaonly could be missing, 
            // in  which case the 'Beat.Auth.Host' should hold the full path of the host url
            string rootUri = $"{beatAuthHost}{beatAuthPort}/{beatAuthApiPrefix}".TrimEnd();

            // NOTE: the root url should end with a "/" so that the path would be properlty appended by the HttpClient class to form a full url.
            // if the ending slash is missing, the last word after the last existing slash would be dropped, then the path gets appended rendering a wrong url.
            if (rootUri[rootUri.Length - 1] != '/')
            {
                rootUri += "/";
            }
            return rootUri;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// When calling this instance method, the caller must dispose of the BeatAuthHttpServiceBase class object in a using statement or call Dispose direclty.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SsoActionResult> ValidateSsoAsync(SsoRequest request)
        {
            return await HttpPostAsync<SsoRequest, SsoActionResult>($"ssoaccess", request);
        }

        /// <summary>
        /// When calling this instance method, the caller must dispose of the BeatAuthHttpServiceBase class object in a using statement or call Dispose direclty.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SsoAuthUrlResult> BuildAuthUrl(SsoAuthUriRequest request)
        {
            return await HttpPostAsync<SsoAuthUriRequest, SsoAuthUrlResult>($"getssourl", request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SsoActionResult> ValidateSingleSignOn(SsoModel request)
        {
            return await HttpPostAsync<SsoModel, SsoActionResult>($"validatesso", request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SsoTokenResult> GetSsoToken(SsoTokenRequest request)
        {
            return await HttpPostAsync<SsoTokenRequest, SsoTokenResult>($"getssotoken", request);
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<SsoActionResult> ValidateSsoAsyncStatic(SsoRequest request)
        {
            using (var httpService = new BeatAuthHttpService())
            {
                return await httpService.HttpPostAsync<SsoRequest, SsoActionResult>($"ssoaccess", request);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<SsoAuthUrlResult> BuildAuthUrlStatic(SsoAuthUriRequest request)
        {
            using (var httpService = new BeatAuthHttpService())
            {
                return await httpService.HttpPostAsync<SsoAuthUriRequest, SsoAuthUrlResult>($"getssourl", request);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<SsoActionResult> ValidateSingleSignOnStatic(SsoModel request)
        {
            using (var httpService = new BeatAuthHttpService())
            {
                return await httpService.HttpPostAsync<SsoModel, SsoActionResult>($"validatesso", request);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<SsoTokenResult> GetSsoTokenStatic(SsoTokenRequest request)
        {
            using (var httpService = new BeatAuthHttpService())
            {
                return await httpService.HttpPostAsync<SsoTokenRequest, SsoTokenResult>($"getssotoken", request);
            }
        }

        #endregion
    }
}
