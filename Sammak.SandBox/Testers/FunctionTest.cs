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

        public static void Run()
        {
            new FunctionTest().DictionaryEqualTest();
        }

        private void DictionaryEqualTest()
        {
            var dict1 = new Dictionary<string, string>() { { "key", "val" } };
            var dict2 = new Dictionary<string, string>() { { "key", "val" } };
            var sameType = dict1.Equals(dict2);
            ConsoleDisplay.ShowObject(sameType, nameof(sameType));
        }

        private void GuidStrTest()
        {
            var guidStr = "1234";
            var guid = new Guid(guidStr);
        }

        //private void ComparableDictionaryEqualityTest()
        //{
        //    ComparableDictionary<string, string> dict1 = new ComparableDictionary<string, string>() { { "key", "val1" } };
        //    ComparableDictionary<string, string>  dict2 = new ComparableDictionary<string, string>() { { "key2", "val2" } };
        //    ComparableDictionary<string, string>  dict3 = new ComparableDictionary<string, string>() { { "key", "val1" } };
        //    //dict1 = null;
        //    //dict3 = null;
        //    //dict2 = dict3;
        //    CompareTwoComparableDictionaries(dict1, dict2);
        //}

        //private void CompareTwoComparableDictionaries(IComparableDictionary<string, string> dict1, IComparableDictionary<string, string> dict2)
        //{
        //    var eq = dict1 == dict2;
        //    //eq = dict1.Equals(dict2);
        //    ConsoleDisplay.ShowObject(eq, nameof(eq));

        //    var dict = (ComparableDictionary<string, string>)dict1;
        //    var sameType = dict != null;
        //    ConsoleDisplay.ShowObject(sameType, nameof(sameType));
        //}

        //private void EqualityTest()
        //{
        //    //ConsoleDisplay.ShowObject(configuration, nameof(item1));


        //ExternalMessagePayload item1 = new ExternalMessagePayload
        //    {
        //        Metadata = new ComparableDictionary<string, string>() { { "key", "val1" },  { "key2", "val2" } }
        //    };
        //    ExternalMessagePayload item2 = new ExternalMessagePayload
        //    {
        //        Metadata = new ComparableDictionary<string, string>() { { "key", "val1" } }
        //    };
        //    //item1 = null;
        //    //item2 = null;
        //    item2 = item1;
        //    var eq = item1 == item2;
        //    ConsoleDisplay.ShowObject(item1.Metadata["key2"], nameof(item1));
        //    //var eq = item1.Equals(item2);
        //    ConsoleDisplay.ShowObject(eq, nameof(eq));
        //    item2.Metadata["key2"] = "value3";
        //    eq = item1 == item2;
        //    ConsoleDisplay.ShowObject(eq, nameof(eq));
        //    ConsoleDisplay.ShowObject(item1.Metadata["key2"], nameof(item1.Metadata));
        //}

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