using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sammak.SandBox.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpService : IDisposable
    {
        #region Private Members
        private Dictionary<string, object> _inMemoryCache = new Dictionary<string, object>();
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string RootUri { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        protected HttpClient Client { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootUri"></param>
        public HttpService(string rootUrl)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                UseDefaultCredentials = true
            };
            RootUri = rootUrl;
            Client = new HttpClient(handler)
            {
                BaseAddress = new Uri(RootUri)
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The Client object instantiated by this class is a disposable one.  need to make sure it is disposed of properly
        /// </summary>
        public void Dispose()
        {
            Client.Dispose();
        }

        /// <summary>
        /// This method makes an http get call to the server.  However, for the frequent call to the same endpoint, if within the same session,
        /// the cache is used.  if the data is already in the cache, then the call is avoided and the cached data is returned.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<TResult> HttpGetAsync<TResult>(string path) where TResult : class
        {
            try
            {
                TResult result = null;
                if (_inMemoryCache.ContainsKey(path))
                {
                    result = _inMemoryCache[path] as TResult;
                    return result;
                }

                HttpResponseMessage response = await Client.GetAsync(path);
                await response.EnsureSuccessStatusCodeAsync();

                var responseString = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<TResult>(responseString);

                // place the result into the cache for possible future invocation of the same endpoint
                _inMemoryCache[path] = result;

                return result;
            }
            catch (WebException ex)
            {
                string errorMessage = WebExceptionMessage(path, ex);
                throw new Exception(errorMessage, ex);
            }
            catch (Exception ex)
            {
                var errorMessage = $"An Exception has been caught for: {RootUri}{path}";
                throw new Exception(errorMessage, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPayload"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="path"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<TResult> HttpPostAsync<TPayload, TResult>(string path, TPayload payload) where TPayload : class where TResult : class
        {
            TResult result = null;
            try
            {
                var payloadJson = JsonConvert.SerializeObject(payload, Formatting.None);
                var payloadStringContent = new StringContent(payloadJson, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync(path, payloadStringContent);
                await response.EnsureSuccessStatusCodeAsync();

                var responseString = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<TResult>(responseString);

                return result;
            }
            catch (WebException ex)
            {
                string errorMessage = WebExceptionMessage(path, ex);
                throw (new Exception(errorMessage, ex));
            }
            catch (Exception ex)
            {
                var errorMessage = $"Unsuccess Response for: {RootUri}{path}";
                throw (new Exception(errorMessage, ex));
            }
        }


        #endregion

        #region Private Methods

        private string WebExceptionMessage(string path, WebException ex)
        {
            var errorMessage = $"WebException has been caught  for: {RootUri}{path}";
            errorMessage += $"{Environment.NewLine}webExcp: {ex.ToString()}";

            WebExceptionStatus status = ex.Status;
            if (status == WebExceptionStatus.ProtocolError)
            {
                errorMessage += $"{Environment.NewLine}The server returned protocol error ";
                HttpWebResponse httpResponse = (HttpWebResponse)ex.Response;
                errorMessage += $"{Environment.NewLine}{(int)httpResponse.StatusCode} - {httpResponse.StatusCode}";
            }

            return errorMessage;
        }
        #endregion
    }
}
