using Newtonsoft.Json;
using System;

namespace Sammak.SandBox.Helpers
{
    public class ConsoleDisplay
    {
        private static JsonSerializerSettings _ipConverterSettings;
        static ConsoleDisplay()
        {
            _ipConverterSettings = new JsonSerializerSettings();
            _ipConverterSettings.Converters.Add(new IPAddressConverter());
            _ipConverterSettings.Converters.Add(new IPEndPointConverter());
            _ipConverterSettings.Formatting = Formatting.Indented;
        }

        public static void ShowObject<T>(T instance, string nameOfInstance)
        {
            Console.WriteLine($"{nameOfInstance}:");
            Console.WriteLine(JsonConvert.SerializeObject(instance, Formatting.Indented));
        }

         public static void ShowText(string message, string nameOfInstance)
        {
            Console.WriteLine($"{nameOfInstance}: {message}");
        }

         public static void ShowHex(long number, string nameOfInstance)
        {
            Console.WriteLine($"{nameOfInstance}: {Environment.NewLine}\t{number:X}");
        }

         public static void ShowUlongHex(ulong number, string nameOfInstance)
        {
            Console.WriteLine($"{nameOfInstance}: {Environment.NewLine}\t{number:X}");
        }

       public static void ShowIPAddress<T>(T instance, string nameOfInstance)
        {
            Console.WriteLine($"{nameOfInstance}:");
            Console.WriteLine(JsonConvert.SerializeObject(instance, _ipConverterSettings));
        }

    }
}
