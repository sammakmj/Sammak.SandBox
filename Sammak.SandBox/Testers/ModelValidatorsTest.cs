using FluentValidation;
using Newtonsoft.Json;
using Sammak.SandBox.Exceptions;
using Sammak.SandBox.Helpers;
using Sammak.SandBox.Models;
using Sammak.SandBox.Models.Sso;
using Sammak.SandBox.Models.TokenString;
using Sammak.SandBox.Models.UserIdentity;
using Sammak.SandBox.Models.UserInfo;
using Sammak.SandBox.Models.UserInfoAction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Sammak.SandBox.Testers
{
    internal class ModelValidatorsTest
    {
        public string UserName { get; set; }
        public string Domain { get; set; }

       public static void Run()
        {
            new ModelValidatorsTest().UriTest();

            //Console.WriteLine($": {}");
        }

        private void UriBuilderTest()
        {
            //Uri url = new Uri("http://localhost/rest/something/browse").
            Uri url = new Uri("/Shared/Error").
                      AddQueryStringParam("page", "0").
                      AddQueryStringParam("pageSize", "200");
            ConsoleDisplay.ShowObject(url, nameof(url));
            url = url.AddQueryStringParam("id", 555.ToString());
            ConsoleDisplay.ShowObject(url, nameof(url));
        }

        private void UriTest()
        {
            string authHost = ConfigurationManager.AppSettings["Auth.Host"];
            string authPort = ConfigurationManager.AppSettings["Auth.Port"];
            string authApiPrefix = ConfigurationManager.AppSettings["Auth.ApiPrefix"];

            if (string.IsNullOrWhiteSpace(authHost) || authHost.Length < 1)
            {
                throw new Exception("The 'Auth.Host' URL define is missing from the config file!");
            }

            // NOTE: the 'Auth.Port' and/or 'Auth.ApiPrefix' defines optiaonly could be missing, 
            // in  which case the 'Auth.Host' should hold the full path of the host url
            string rootUri = $"{authHost}{authPort}/{authApiPrefix}".TrimEnd();

            // NOTE: the root url should end with a "/" so that the path would be properlty appended by the HttpClient class to form a full url.
            // if the ending slash is missing, the last word after the last existing slash would be dropped, then the path gets appended rendering a wrong url.
            if (rootUri[rootUri.Length - 1] != '/')
            {
                rootUri += "/";
            }
            var uri = new Uri(rootUri);
            ConsoleDisplay.ShowObject(rootUri,  nameof(rootUri));


            return;
            //var client = new HttpClient { BaseAddress = uri };
            //ConsoleDisplay.ShowObject(client,  nameof(client));

            //var payload = new SsoAuthUriRequest { CallBackUrl = "http://www.xyz.com" };
            //var payloadJson = JsonConvert.SerializeObject(payload, Formatting.None);
            //var payloadStringContent = new StringContent(payloadJson, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = client.PostAsync("getssourl", payloadStringContent).GetAwaiter().GetResult();
            //ConsoleDisplay.ShowObject(response.IsSuccessStatusCode,  nameof(response.IsSuccessStatusCode));
            //ConsoleDisplay.ShowObject(response, nameof(response));

            //var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            //var result = JsonConvert.DeserializeObject<SsoAuthUrlResult>(responseString);
            //ConsoleDisplay.ShowObject(result,  nameof(result));

        }

        private void ExceptionStatusCodeTest()
        {
            Exception ex = GetSimpleHttpResponseException();
            Console.WriteLine($"ex type: {ex.GetType()}");
            Console.WriteLine($"ex: {JsonConvert.SerializeObject(ex, Formatting.Indented)}");
        }

        private Exception GetSimpleHttpResponseException()
        {
            //var ex = new Exception("basic exception");
            var ex = new SimpleHttpResponseException(HttpStatusCode.NotFound, "Target missing");
            return ex;
        }

        private void EnumTest()
        {
            var ssoResultType = SsoResultType.Failed;
            ssoResultType = (int)SsoResultType.Success;
            //var i = (int)SsoResultType.NoActivePracticeForDomain;
            //ssoResultType = 5;
            Console.WriteLine($"ssoResultType: {ssoResultType}; ToString = {ssoResultType.ToString()}");
        }

        private void ValidateUrlUsingRegexTest()
        {
            var pattern = new Regex(@"http(s)?://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\\'\\,]*)?");

            var url = "http:%2F%2Flocalhost:4987%2FLoginCallback.ashx&response_type=code&scope=openid";
            Console.WriteLine($"Input string for URL: {url}");
            Console.WriteLine();

            Console.WriteLine($"Method: Regex");
            var match = pattern.IsMatch(url);
            Console.WriteLine($"match: {match}");
            Console.WriteLine();

            string decodedUrl = System.Web.HttpUtility.UrlDecode(url);
            match = pattern.IsMatch(decodedUrl);
            Console.WriteLine($"decodedUrl match: {decodedUrl}");
            Console.WriteLine($"match: {match}");
            Console.WriteLine();
        }

        private void ShowResultTest()
        {
            //var result = new SsoActionResult { ResultEnum = SsoResultType.UserNotFound };
            var result = new SsoActionResult { ResultType = (int)SsoResultType.UserNotFound };
            Console.WriteLine($"result: {JsonConvert.SerializeObject(result, Formatting.Indented)}");
        }

        private void ValidateUrlTest()
        {
            // "CallBackUrl": "http://localhost:4987/LoginCallback.ashx",
            //var url = @"c:\directory\filename";
            //var url = "https://cardionet.com";
            var url = "http:%2F%2Flocalhost:4987%2FLoginCallback.ashx&response_type=code&scope=openid";
            //var url = "http://localhost:4987/LoginCallback.ashx&response_type=code&scope=openid";

            Console.WriteLine($"Input string for URL= {url}");
            Console.WriteLine();

            Console.WriteLine($"Method: Uri.IsWellFormedUriString");
            var isWellFormedUriString = Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute); ;
            Console.WriteLine($"isWellFormedUriString: {isWellFormedUriString}");
            Console.WriteLine();

            var decodedUrl = System.Web.HttpUtility.UrlDecode(url);
            Console.WriteLine($"decodedUrl: {decodedUrl}");
            isWellFormedUriString = Uri.IsWellFormedUriString(decodedUrl, UriKind.RelativeOrAbsolute); ;
            Console.WriteLine($"decodedUrl: {isWellFormedUriString}");
            Console.WriteLine();

            var decodedUrl2 = System.Web.HttpUtility.UrlDecode(decodedUrl);
            Console.WriteLine($"decodedUrl2: {decodedUrl2}");
            isWellFormedUriString = Uri.IsWellFormedUriString(decodedUrl2, UriKind.RelativeOrAbsolute); ;
            Console.WriteLine($"decodedUrl2: {isWellFormedUriString}");
            Console.WriteLine();

        }

        private void SsoAuthUriRequestValidatorTest()
        {
            var url = "http:%2F%2Flocalhost:4987%2FLoginCallback.ashx&response_type=code&scope=openid";
            //var url = "c:\\directory\\filename";
            //var url = "https://cardionet.com";
            var req = new SsoAuthUriRequest
            {
                //CallBackUrl = url,
            State = string.Empty
            };
            var valid = ValidateAndShowValidationResult(req, nameof(req));
            //if(valid)
            //{
            //    if (Uri.TryCreate(req.CallBackUrl, UriKind.Absolute, out Uri validatedUri)
            //        //&& result.Scheme == Uri.UriSchemeHttp
            //        )
            //    {
            //        Console.WriteLine($"AbsolutePath:  {validatedUri.AbsolutePath}");
            //        Console.WriteLine($"AbsoluteUri:  {validatedUri.AbsoluteUri}");
            //        Console.WriteLine($"ToString:  {validatedUri.ToString()}");
            //        Console.WriteLine($"PathAndQuery:  {validatedUri.PathAndQuery}");
            //        Console.WriteLine($"Scheme:  {validatedUri.Scheme}");
            //        //Console.WriteLine($":  {validatedUri}");
            //        //Console.WriteLine($":  {validatedUri}");
            //        //Console.WriteLine($":  {validatedUri}");
            //        //Use the valid Uri here
            //        ConsoleDisplay.ShowObject(validatedUri, nameof(validatedUri));
            //    }
            //}
        }

        private void SsoModelTest()
        {
            // {"is_emory_user":false,"user_id":"ad|Auth0Dev1Temp|ea441d09-2153-4e39-9d61-668af1b18605","user_nickname":"jwhitfill2","user_name":"Jody Whitfill","user_email":"JWhitfill2@cardionet.com","user_dc":"CN=Jody Whitfill,OU=Company Users,DC=Auth0Dev1Temp,DC=com","sso_user":false}
            // https://emory.com
            var claims = new List<Claim>
            {
                //new Claim ("user_dc", "CN=Jody Whitfill,OU=Company Users,DC=Auth0Dev1Temp,DC=com"),
                //new Claim ("user_nickname", "jwhitfill2"),
                //new Claim ("user_name", "John Doe"),
                //new Claim ("user_id", "E6135E34-CC95-4576-8C93-D5404999802B"),
                //new Claim ("user_email", "joe@example.com"),
                //new Claim ("https://emory.com", "{\"https://emory.com\":false,\"user_id\":\"ad|Auth0Dev1Temp|ea441d09-2153-4e39-9d61-668af1b18605\",\"user_nickname\":\"jwhitfill2\",\"user_name\":\"Jody Whitfill\",\"user_email\":\"JWhitfill2@cardionet.com\",\"user_dc\":\"CN=Jody Whitfill,OU=Company Users,DC=Auth0Dev1Temp,DC=com\",\"sso_user\":false}"),
                new Claim ("https://emory.com", "{\"https://emory.com\":false,\"user_id\":\"ad|Auth0Dev1Temp\",\"user_nickname\":\"jwhitfill2\",\"user_dc\":\"CN=Jody Whitfill,OU=Company Users,DC=Auth0Dev1Temp,DC=com\",\"sso_user\":false}"),
                //new Claim ("", ""),
                //new Claim ("", ""),
                //new Claim ("", ""),
                //new Claim ("", ""),
            };

            var ssoModel = new SsoModel(claims)
                {
                    AppId = "E6135E34-CC95-4576-8C93-D5404999802B",
                    IsEmoryUser = true,
                    IsSsoUser = true
                };
            //ssoModel.UserName = "m@a.b"; // string.Empty;
            //ssoModel.NickName = "abc";
            ValidateAndShowValidationResult(ssoModel, nameof(ssoModel));
        }

        private void BuildNameAndDomain()
        {
            Dictionary<string, string> propertyValues = new Dictionary<string, string>
            {//
                ["user_dc"] = "CN=Jody Whitfill,OU=Company Users,DC=Auth0Dev1Temp,DC=com",
                ["user_nickname"] = "jwhitfill2"
            };
            BuildUserNameAndDomain(propertyValues);
            Console.WriteLine($"{JsonConvert.SerializeObject(propertyValues, Formatting.Indented)}");
            Console.WriteLine($"UserName: {UserName}");
            Console.WriteLine($"Domain: {Domain}");
        }

        private void StringSplitTest()
        {
            var input = "CN=Jody Whitfill,OU=Company Users,DC=Auth0Dev1Temp,DC=com";
            var output = GetDomainName(input);
            Console.WriteLine($"Input: \t {input}");
            Console.WriteLine($"Output: {output}");

        }

        private string GetStringProperty(string propertyKey, Dictionary<string, string> propertyValues)
        {
            if (propertyValues.ContainsKey(propertyKey))
            {
                return propertyValues[propertyKey];
            }
            return string.Empty;
        }

        private void BuildUserNameAndDomain(Dictionary<string, string> propertyValues)
        {
            //the "user_dc claim would carry in a text like:  "CN=Jody Whitfill,OU=Company Users,DC=Auth0Dev1Temp,DC=com";
            // the result Domain would be "Auth0Dev1Temp" and Username would be <nickname>@Auth0Dev1Temp.com
            var dcClaim = GetStringProperty("user_dc", propertyValues);
            var nickName = GetStringProperty("user_nickname", propertyValues);
            var domain = string.Empty;
            var extension = string.Empty;
            if (!string.IsNullOrWhiteSpace(dcClaim))
            {
                var indexOfDomain = dcClaim.IndexOf("DC");
                if (indexOfDomain != -1)
                {
                    indexOfDomain += 3;  //  skip the "DC=" part
                    var indexOfComma = dcClaim.IndexOf(",", indexOfDomain);
                    if (indexOfComma != -1)
                    {
                        domain = dcClaim.Substring(indexOfDomain, indexOfComma - indexOfDomain);
                    }
                    // now find the domain extension
                    var indexOfExtension = dcClaim.IndexOf("DC", indexOfDomain + domain.Length);
                    if (indexOfExtension != -1)
                    {
                        indexOfExtension += 3;  //  skip the "DC=" part
                        var extesionLen = dcClaim.Length - indexOfExtension;
                        if (extesionLen > 0)
                        {
                            extension = dcClaim.Substring(indexOfExtension, extesionLen);
                        }
                    }
                }
            }

            // rebuild the username
            UserName = $"{nickName}@{domain}.{extension}";
            Domain = domain;
        }
        private string GetDomainName(string input)
        {
            // search for the domain name which shold be the text after the first occurance of "DC=" unitl the following ","
            //example: input = "CN=Jody Whitfill,OU=Company Users,DC=Auth0Dev1Temp,DC=com";
            // example result would be "Auth0Dev1Temp"
            string output = string.Empty;
            if(!string.IsNullOrWhiteSpace(input))
            {
                var index = input.IndexOf("DC");
                if(index > 3)
                {
                    // skip the "DC=" part
                    var startIndex = index + 3;
                    var indexOfComma = input.IndexOf(",", startIndex);
                    if(indexOfComma != -1)
                    {
                        output = input.Substring(startIndex, indexOfComma - startIndex);
                    }
                }
            }
            return output;
        }

        private void IsNullOrWhiteSpaceTest()
        {
            string str = "\t";
            var isWhiteSpace = string.IsNullOrWhiteSpace(str); //true
            var isNullOrEmpty = string.IsNullOrEmpty(str); //false
            Console.WriteLine($"string: {str};   isWhiteSpace = {isWhiteSpace}, isNullOrEmpty = {isNullOrEmpty}");
            str = "    ";
            isWhiteSpace = string.IsNullOrWhiteSpace(str); //true
           isNullOrEmpty =  string.IsNullOrEmpty(str); //false
            Console.WriteLine($"string: {str};   isWhiteSpace = {isWhiteSpace}, isNullOrEmpty = {isNullOrEmpty}");

            str = "\r\n";
            isWhiteSpace = string.IsNullOrWhiteSpace(str); //true
            isNullOrEmpty = string.IsNullOrEmpty(str); //false 
            Console.WriteLine($"string: {str};   isWhiteSpace = {isWhiteSpace}, isNullOrEmpty = {isNullOrEmpty}");
        }

            private void UserInfoActionModelTest()
        {
            //var userIdentityString = @"""Austin@Powers""@example.com";
            var userIdentityString = @"!def!xyz%abc@example.com";
            //var userIdentityString = @"""test""blah""@example.com";
            //var userIdentityString = "myname@.com";
            UserInfoActionModel userInfo = new UserInfoActionModel
            {
                UserName = "123",
                Name = "",
                Email = "",
                Domain = ""
            };

        //ValidateAndShowValidationResult(user, nameof(user));
        ValidateAndShowValidationResult(userInfo, nameof(userInfo), true);
        }

        private void UserIdentityModelTest()
        {
            var userIdentityString = @"!def!xyz%abc@example.com";
            UserIdentityModel userIdentityModel = new UserIdentityModel(userIdentityString);
            ValidateAndShowValidationResult(userIdentityModel, nameof(userIdentityModel));
        }

        private void UserInfoModelTest()
        {
            //var userIdentityString = @"""Austin@Powers""@example.com";
            var userIdentityString = @"!def!xyz%abc@example.com";
            //var userIdentityString = @"""test""blah""@example.com";
            //var userIdentityString = "myname@.com";
            UserInfoModel userInfo = new UserInfoModel(userIdentityString);

            //ValidateAndShowValidationResult(user, nameof(user));
            ValidateAndShowValidationResult(userInfo, nameof(userInfo), true);
        }

        private void TokenTest()
        {
            var token = (TokenStringModel)@"header.payload.signature.";
            ValidateAndShowValidationResult(token, nameof(token));
        }

        private bool ValidateAndShowValidationResult<T>(T instance, string nameOfInstance, bool staticValidation = false)
            where T : ModelBase<T>
        {
            string validationMessage;
            var valid = false;
            if (staticValidation)
            {
                valid = ModelBase<T>.Validate(instance, out validationMessage);
            }
            else
            {
                valid = instance.Validate(out validationMessage);
            }

            Console.WriteLine($"{nameOfInstance} is:");
            Console.WriteLine($"{JsonConvert.SerializeObject(instance, Formatting.Indented)}");
            Console.WriteLine();
            Console.WriteLine($"Validation result: {validationMessage}");
            return valid;
        }

        //private void SerializerTest()
        //{
        //    var request = new SsoModel { UserName = "Jeff", AppId = Guid.NewGuid().ToString() };
        //    var serializer = new JavaScriptSerializer();
        //    var json = serializer.Serialize(request);
        //    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
        //    Console.WriteLine($"JsonConvert:  \n{JsonConvert.SerializeObject(request)}");
        //    Console.WriteLine($"JavaScriptSerializer: \n{json}");

        //}

        private void SSOEnabledTest()
        {
            var ss0Enabled = ApplicationHelper.GetAppSettingValue("SSOEnabled").Equals("True", StringComparison.InvariantCultureIgnoreCase);
            Console.WriteLine($"ss0Enabled: {ss0Enabled}");
        }

        private void ValueValidatorTest()
        {
            Console.WriteLine("Test true");
            new ValueValidator().ValidateAndThrow(new BoolDataType { Value = true });

            Console.WriteLine("Test null");
            new ValueValidator().ValidateAndThrow(new BoolDataType { Value = null });

            Console.WriteLine("Test false");
            new ValueValidator().ValidateAndThrow(new BoolDataType { Value = false });
        }

    }

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

        //public override string ToString()
        //{
        //    var res = base.ToString();
        //    return res;
        //}
    }

    public class ValueValidator : AbstractValidator<DataType>
    {
        public ValueValidator()
        {
            RuleFor(x => x.Value)
                .SetValidator(x => new BoolDataTypeValidator())
                .When(x => x is BoolDataType);
        }
    }

    public class BoolDataTypeValidator : AbstractValidator<object>
    {
        public BoolDataTypeValidator()
        {
            Console.WriteLine("BoolDataTypeValidator considered");
        }
    }

    public class DataType
    {
        public object Value { get; set; }
    }

    public class BoolDataType : DataType
    {
    }
}
