using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sammak.SandBox.Testers
{
    class UserDataTester
    {
        public static void Run()
        {
            new UserDataTester().UserDataTest();
        }

        private void UserDataTest()
        {
            string json = "{\"user_id\":\"ad|Auth0-MJS-Test|759006bf-8d53-4431-9b25-bd07affc1131\",\"user_name\":\"sammakmj\",\"name\":\"MJ Sammak\",\"email\":\"sammakmj@emory.com\",\"connection\":\"Auth0-MJS-Test\"}";
            var userData = new UserData(json);
            var result = JsonConvert.SerializeObject(userData, Formatting.Indented);
            Console.WriteLine($"result = {result}");
        }

    }

    #region Private helper class
    public class UserData
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; } = "";
        public string Name { get; private set; } = "";
        public string Email { get; private set; }
        public string Domain { get; private set; } = "";
        public bool IsEmoryUser { get; private set; }
        public bool IsSsoUser { get; private set; }

        public UserData(string customKVPairs)
        {
            //var claimsJson = JsonConvert.SerializeObject(claims, Formatting.Indented);
            //var customKVPairs = claims.Where(x => x.Type == EMORY_NAMESPACE).FirstOrDefault()?.Value;
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

    }

    #endregion

}
