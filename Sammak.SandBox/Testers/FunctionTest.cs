using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sammak.SandBox.Common;
using Sammak.SandBox.Helpers;
using Sammak.SandBox.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sammak.SandBox.Testers
{
    public class FunctionTest
    {
        private readonly ILogger<FunctionTest> _logger;

        //public FunctionTest(ILogger<FunctionTest> logger)
        //{
        //    _logger = logger;
        //}

        public static void Run()
        {
            //var functionTest = AppData.ServiceProvider.GetService<FunctionTest>();
            new FunctionTest().ObservableCollectionTest();
            //functionTest.AppSettingTest();
        }

        private DeviceLog LogForDeviceActivatedInMsa(string prescriptionId)
        {
            return new DeviceLog
            {
                Description = $"Device was activated in MSA ({prescriptionId})"
            };
        }

        private void ObservableCollectionTest()
        {
            ObservableCollection<DeviceLog> deviceLogs = new ObservableCollection<DeviceLog>();

            var log = LogForDeviceActivatedInMsa("My Prescription");
            for (int i = 0; i < 3; i++)
            {
                deviceLogs.Add(log);
            }

            ConsoleDisplay.ShowObject(deviceLogs, nameof(deviceLogs));
        }

        private void ListReturnTest()
        {
            List<string> messages = GenMessages();
            ConsoleDisplay.ShowObject(messages, nameof(messages));
        }

        private List<string> GenMessages()
        {
            List<string> messages = new List<string>();
            var any = messages.Any();
            messages.Add("m1");
            any = messages.Any();
            messages.Add("m2");
            any = messages.Any();
            string x = messages
                .Aggregate(
                    "Messages are",
                    (message, next) => $"{message}, {next}"
                    );
            Console.WriteLine(x);
            return messages;
        }

        private void PartNumberTest()
        {
            PreBuiltKitDetail preBuiltKitDetail = new PreBuiltKitDetail
            {
                PartNumber = "100-0030-01"
            };
            ConsoleDisplay.ShowObject(preBuiltKitDetail, nameof(preBuiltKitDetail));
        }

        private void IntParseWithLimitTest()
        {
            ConsoleDisplay.ShowObject(int.MinValue, nameof(int.MinValue));
            ConsoleDisplay.ShowObject(int.MaxValue, nameof(int.MaxValue));
            ConsoleDisplay.ShowObject(long.MinValue, nameof(long.MinValue));
            ConsoleDisplay.ShowObject(long.MaxValue, nameof(long.MaxValue));
            string number = long.MaxValue.ToString();
            ConsoleDisplay.ShowText(number, nameof(number));
        }

        private void LongParseWithLimitTest()
        {
            ulong ulNumber = 163245617943825;
            try
            {
                long number1 = (long)ulNumber;
                Console.WriteLine(number1);
            }
            catch (OverflowException)
            {
                Console.WriteLine("{0} is out of range of an Int64.", ulNumber);
            }

            long[] numbersToConvert = { 162345, 32183, -54000, long.MaxValue / 2 };
            int newNumber;
            foreach (long number in numbersToConvert)
            {
                if (number >= int.MinValue && number <= int.MaxValue)
                {
                    newNumber = Convert.ToInt32(number);
                    Console.WriteLine("Successfully converted {0} to an Int32.",
                                      newNumber);
                }
                else
                {
                    Console.WriteLine("Unable to convert {0} to an Int32.", number);
                }
            }
        }

        //private void LongParseTest()
        //{
        //    bool valid = true;
        //    ulong? output;
        //    ulong number = 18446744073709551615;  //ulong.MaxValue;

        //    string input = "184467440737095516151"; // number.ToString();
        //    string current = input;
        //    ConsoleDisplay.ShowUlongHex(number, nameof(number));

        //    valid = ulong.TryParse(current, out ulong tempLong);
        //    ConsoleDisplay.ShowObject(valid, nameof(valid));

        //    //number = number +2;
        //    //Console.WriteLine($"{number:x}".ToUpper() + $"     {nameof(number)}:    ");
        //    //Console.WriteLine($"{number.ToString()}" + $"     {nameof(input)}:    ");
        //    string last = input;
        //    return;
        //    int count = 0;
        //    while (valid)
        //    {
        //        count++;
        //        valid = ulong.TryParse(current, out tempLong);
        //        if (valid)
        //        {
        //            if (tempLong == 0)
        //            {
        //                break;
        //            }

        //            last = current;
        //            ulong currentNum = tempLong * 2;
        //            current = currentNum.ToString();
        //            //current = current + "0";
        //        }
        //    }

        //    Console.WriteLine($"{last}" + "     last:      ");
        //    Console.WriteLine($"{current}" + "    current: ");
        //    Console.WriteLine($"{tempLong}" + "     tempLong:    ");
        //    Console.WriteLine($"{count}" + "     count:    ");
        //    //output = long.TryParse(input, out tempLong) ? (long?)tempLong : null;
        //    //Console.WriteLine($"input:     {input}");
        //    //Console.WriteLine($"last:       {last}");
        //    //Console.WriteLine($"current:  {current}");
        //    //ConsoleDisplay.ShowText(input, nameof(input));
        //    //ConsoleDisplay.ShowText(last, nameof(last));
        //    //ConsoleDisplay.ShowText(current, nameof(current));

        //    output = valid ? (ulong?)tempLong : null;
        //    ConsoleDisplay.ShowObject(valid, nameof(valid));
        //    ConsoleDisplay.ShowObject(output, nameof(output));
        //    //output = long.TryParse(input, out tempLong) ? (long?)tempLong : null;
        //    //ConsoleDisplay.ShowObject(output, nameof(LongParseTest));
        //}

        private void DictionaryEqualTest()
        {
            Dictionary<string, string> dict1 = new Dictionary<string, string>() { { "key", "val" } };
            Dictionary<string, string> dict2 = new Dictionary<string, string>() { { "key", "val" } };
            bool sameType = dict1.Equals(dict2);
            ConsoleDisplay.ShowObject(sameType, nameof(sameType));
        }

        private void GuidStrTest()
        {
            string guidStr = "1234";
            Guid guid = new Guid(guidStr);
        }

        private void ComparableDictionaryEqualityTest()
        {
            ComparableDictionary<string, string> dict1 = new ComparableDictionary<string, string>() { { "key", "val" } };
            ComparableDictionary<string, object> dict2 = new ComparableDictionary<string, object>() { { "key", "val" } };
            ComparableDictionary<string, string> dict3 = new ComparableDictionary<string, string>() { { "key", "val1" } };
            //dict1 = null;
            //dict3 = null;
            //dict2 = dict3;
            //CompareTwoComparableDictionaries(dict1, dict2);
            //var eq = dict1 == dict2;
            bool eq = dict1.Equals(dict2);
            ConsoleDisplay.ShowObject(eq, nameof(eq));
        }

        private void CompareTwoComparableDictionaries(ComparableDictionary<string, string> dict1, ComparableDictionary<string, string> dict2)
        {
            bool eq = dict1 == dict2;
            //eq = dict1.Equals(dict2);
            ConsoleDisplay.ShowObject(eq, nameof(eq));

            //var dict = (ComparableDictionary<string, string>)dict1;
            //var sameType = dict != null;
            //ConsoleDisplay.ShowObject(sameType, nameof(sameType));
        }

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

        private void GetDataSet()
        {
            string[] ids = new string[] { "91860ff9-34fb-4a0b-8ae7-f88d1e9a5d1f", "12345678-34fb-4a0b-8ae7-f88d1e9a5d1f" };
            List<string> ids2 = new List<string> { "91860ff9-34fb-4a0b-8ae7-f88d1e9a5d1f", "12345678-34fb-4a0b-8ae7-f88d1e9a5d1f" };
            //var dict = new Dictionary<string, string>();
            //{
            //    dict["CK24"] = "true";
            //    dict["ApplicationIds"] = @"[""91860ff9-34fb-4a0b-8ae7-f88d1e9a5d1f"", ""12345678-34fb-4a0b-8ae7-f88d1e9a5d1f""]";
            //};

            Dictionary<string, string> featureConfigs = new Dictionary<string, string>
            {
                ["CK24"] = "true",
                ["CKExtended"] = "true",
                ["ePatch24"] = "true",
                ["ePatchExtended"] = "true",
                ["CK48"] = "false",
                ["ePatch48"] = "false",
                ["ApplicationIds"] = JsonConvert.SerializeObject(ids2)
            };
            string key = "ApplicationIds";
            bool exists = featureConfigs.ContainsKey(key);

            string targetId = "91860ff9-34fb-4a0b-8ae6-f88d1e9a5d1f";

            string appIdsString = featureConfigs
                .FirstOrDefault(d => d.Key == "ApplicationId").Value;
            Console.WriteLine($"appIdsString:  {appIdsString}");

            appIdsString = null;
            bool found = false;
            try
            {
                List<string> appIds = appIdsString == null ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(appIdsString);
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