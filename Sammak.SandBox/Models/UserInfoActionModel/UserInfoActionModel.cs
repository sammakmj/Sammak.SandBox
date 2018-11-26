using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Sammak.SandBox.Models.UserInfoAction
{
    public class UserInfoActionModel : ModelBase<UserInfoActionModel>
    {
        #region private members and constants

        private const string EMORY_NAMESPACE = "https://emory.com";

        #endregion

        #region Public Properties

        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Domain { get; set; }

        public bool IsEmoryUser { get; set; }

        public bool IsSsoUser { get; set; }

        #endregion

        #region constructor

        public UserInfoActionModel()
        {
        }

        public UserInfoActionModel(IEnumerable<Claim> claims)
        {
            var customKVPairs = claims?.Where(x => x.Type == EMORY_NAMESPACE).FirstOrDefault()?.Value;
            if (!string.IsNullOrEmpty(customKVPairs))
            {
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(customKVPairs);

                Id = GetId("user_id", values);
                UserName = GetStringProperty("user_nickname", values);
                Name = GetStringProperty("user_name", values);
                Email = GetStringProperty("user_email", values);
                IsEmoryUser = GetBooleanProperty("is_emory_user", values);
                IsSsoUser = GetBooleanProperty("sso_user", values);
                ExtractAndSetDomain();
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

        private void ExtractAndSetDomain()
        {
            var emailText = (Email) ?? "";
            if (emailText.Contains("@"))
            {
                string[] split = emailText.Split('@');
                Domain = split.Last().Split('.')[0];
            }
        }

        #endregion

    }

}
