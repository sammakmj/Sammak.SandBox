using Newtonsoft.Json;
using Sammak.SandBox.Helpers;
using Sammak.SandBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sammak.SandBox.Testers
{
    public class FunctionTest
    {

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

        public static void Run()
        {
            new FunctionTest().EqualityTest();
        }

        private void ComparableDictionaryEqualityTest()
        {
            ComparableDictionary<string, string> dict1 = new ComparableDictionary<string, string>() { { "key", "val1" } };
            ComparableDictionary<string, string>  dict2 = new ComparableDictionary<string, string>() { { "key", "val1" } };
            ComparableDictionary<string, string>  dict3 = new ComparableDictionary<string, string>() { { "key", "val1" } };
            dict1 = null;
            dict3 = null;
            dict2 = dict3;
            var eq = dict1 == dict2;
            //eq = dict1.Equals(dict2);
            ConsoleDisplay.ShowObject(eq, nameof(eq));
        }

        private void EqualityTest()
        {
            ExternalMessagePayload item1 = new ExternalMessagePayload
            {
                Metadata = new ComparableDictionary<string, string>() { { "key", "val1" },  { "key2", "val2" } }
            };
            ExternalMessagePayload item2 = new ExternalMessagePayload
            {
                Metadata = new ComparableDictionary<string, string>() { { "key", "val1" } }
            };
            //item1 = null;
            //item2 = null;
            item2 = item1;
            var eq = item1 == item2;
            ConsoleDisplay.ShowObject(item1.Metadata["key2"], nameof(item1));
            //var eq = item1.Equals(item2);
            ConsoleDisplay.ShowObject(eq, nameof(eq));
            item2.Metadata["key2"] = "value3";
            eq = item1 == item2;
            ConsoleDisplay.ShowObject(eq, nameof(eq));
            ConsoleDisplay.ShowObject(item1.Metadata["key2"], nameof(item1.Metadata));
        }

        private void UserDataTest()
        {
            string json = "{\"user_id\":\"ad|Auth0-MJS-Test|759006bf-8d53-4431-9b25-bd07affc1131\",\"user_name\":\"sammakmj\",\"name\":\"MJ Sammak\",\"email\":\"sammakmj@emory.com\",\"connection\":\"Auth0-MJS-Test\"}";
            var userData = new UserData(json);
            var result = JsonConvert.SerializeObject(userData, Formatting.Indented);
            Console.WriteLine($"result = {result}");

        }

        void GetDataSet()
        {
            var ids = new string[]{ "91860ff9-34fb-4a0b-8ae7-f88d1e9a5d1f", "12345678-34fb-4a0b-8ae7-f88d1e9a5d1f" };
            var ids2 = new List<string>{ "91860ff9-34fb-4a0b-8ae7-f88d1e9a5d1f", "12345678-34fb-4a0b-8ae7-f88d1e9a5d1f" };
            //var dict = new Dictionary<string, string>();
            //{
            //    dict["CK24"] = "true";
            //    dict["ApplicationIds"] = @"[""91860ff9-34fb-4a0b-8ae7-f88d1e9a5d1f"", ""12345678-34fb-4a0b-8ae7-f88d1e9a5d1f""]";
            //};

            var featureConfigs = new Dictionary<string, string>
            {
                ["CK24"] = "true",
                ["CKExtended"] = "true",
                ["ePatch24"] = "true",
                ["ePatchExtended"] = "true",
                ["CK48"] = "false",
                ["ePatch48"] = "false",
                ["ApplicationIds"] = JsonConvert.SerializeObject(ids2)
            };
            var key = "ApplicationIds";
            var exists = featureConfigs.ContainsKey(key);

            var targetId = "91860ff9-34fb-4a0b-8ae6-f88d1e9a5d1f";

            var appIdsString = featureConfigs
                .FirstOrDefault(d => d.Key == "ApplicationId").Value;
            Console.WriteLine($"appIdsString:  {appIdsString}");

            appIdsString = null;
            var found = false;
            try
            {
                var appIds = appIdsString == null ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(appIdsString);
                found = appIds.Exists(id => id == targetId);

            }
            catch
            {

            }

            Console.WriteLine($"found:  {found}");
        }

        private static string TryFormatJson(string str)
        {
            try
            {
                object parsedJson = JsonConvert.DeserializeObject(str); // Configuration = JsonConvert.DeserializeObject<Dictionary<string, string>>(featureType.Default)
                return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            }
            catch
            {
                // can't parse JSON, return the original string
                return str;
            }
        }


    }
}