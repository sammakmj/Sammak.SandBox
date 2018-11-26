using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;

namespace Sammak.SandBox.Models.Sso
{
    //public class SsoModel : ModelBase<SsoModel, SsoModelValidator>
    public class SsoModel : ModelBase<SsoModel>
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; private set; }

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

        #region constructors

        public SsoModel()
        {
        }

        public SsoModel(IEnumerable<Claim> claims)
        {
            var emoryNamespace = ConfigurationManager.AppSettings["auth0:ClientNameSpace"];
            var customKVPairs = claims?.Where(x => x.Type == emoryNamespace).FirstOrDefault()?.Value;
            if (!string.IsNullOrWhiteSpace(customKVPairs))
            {
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(customKVPairs);
                BuildUserNameAndDomain(values);
                Id = GetId("user_id", values);
                Name = GetStringProperty("user_name", values);
                NickName = GetStringProperty("user_nickname", values);
                Email = GetStringProperty("user_email", values);
                IsEmoryUser = GetBooleanProperty("is_emory_user", values);
                IsSsoUser = GetBooleanProperty("sso_user", values);

                //UserName = GetStringProperty("user_nickname", values);
                //var dcClaim = GetStringProperty("user_dc", values);
                //Domain = GetDomainName(dcClaim);
            }
        }

        #endregion

        #region Private methods

        private string GetStringProperty(string propertyKey, Dictionary<string, string> propertyValues)
        {
            if (propertyValues.ContainsKey(propertyKey))
            {
                return propertyValues[propertyKey];
            }
            return string.Empty;
        }

        private bool GetBooleanProperty(string propertyKey, Dictionary<string, string> propertyValues)
        {
            var result = false;
            if (propertyValues.ContainsKey(propertyKey))
            {
                var str = propertyValues[propertyKey];
                bool.TryParse(str, out result);
            }
            return result;
        }

        private Guid GetId(string propertyKey, Dictionary<string, string> propertyValues)
        {
            var id = Guid.Empty;
            // id is of  "ad|<connector name>|userid (Guid)" format
            // example: ad|Auth0-MJS-Test|759006bf-8d53-4431-9b25-bd07affc1131
            if (propertyValues.ContainsKey(propertyKey))
            {
                var str = propertyValues[propertyKey];
                int idx = string.IsNullOrEmpty(str) ? -1 : str.LastIndexOf('|');
                if (idx != -1)
                    Guid.TryParse(str.Substring(idx + 1), out id);
            }
            return id;
        }

        private string GetDomainName(string dcClaim)
        {
            // search for the domain name which shold be the text after the first occurance of "DC=" unitl the following ","
            //example: input = "CN=Jody Whitfill,OU=Company Users,DC=Auth0Dev1Temp,DC=com";
            // example result would be "Auth0Dev1Temp"
            string output = string.Empty;
            if (!string.IsNullOrWhiteSpace(dcClaim))
            {
                var index = dcClaim.IndexOf("DC");
                if (index > 3)
                {
                    // skip the "DC=" part
                    var startIndex = index + 3;
                    var indexOfComma = dcClaim.IndexOf(",", startIndex);
                    if (indexOfComma != -1)
                    {
                        output = dcClaim.Substring(startIndex, indexOfComma - startIndex);
                    }
                }
            }
            return output;
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
        #endregion

    }

}
