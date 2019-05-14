using Newtonsoft.Json;
using Sammak.SandBox.Helpers;
using Sammak.SandBox.Inventory.QueueMessaging.MessageBuild;
using Sammak.SandBox.Models;
using System;
using System.Text;

namespace Sammak.SandBox.Testers
{
    public class JsonTester
    {
        public static void Run()
        {
            new JsonTester().ExternalMessageTest();
        }

        private void ExternalMessageTest()
        {
            var orderId = Guid.NewGuid();
            var msg = new NovusExternalMessage
            {
                MessageType = 0,
                SourceSystemId = 3,
                OrderId = orderId.ToString(),
                UserName = "test user"
            };
            ConsoleDisplay.ShowObject(msg, nameof(msg));
            var body = JsonConvert.SerializeObject(msg);
            ConsoleDisplay.ShowText(body, nameof(body));
            var payload = JsonConvert.DeserializeObject<ExternalMessagePayload>(body);
            ConsoleDisplay.ShowObject(payload, nameof(payload));

        }

        private void JsonConverterTest()
        {
            object freeObj = new 
            {
                UserName = "user",
                MessageType = 3,
                Id = "1234"
            };
            int messageType = 4;
            string id = "5678";
            string userName = "user name";

            byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(freeObj));
            var framelist = new FrameBuilder().Build(bytes);

            ConsoleDisplay.ShowObject(bytes, nameof(bytes));

            var payloadStr = JsonConvert.SerializeObject(
                new 
                {
                    userName,
                    messageType,
                    id
                });
            var payload = JsonConvert.DeserializeObject<ExternalMessagePayload>(payloadStr);
            ConsoleDisplay.ShowObject(payload, nameof(payload));
            //var payload = new ExternalMessagePayload
            //{

            //}
        }

        private void BitConverterTest()
        {
            var number = 0x5A;
            //var length = 32;
            var bytes = BitConverter.GetBytes(number);
            ConsoleDisplay.ShowObject(bytes, nameof(bytes));

            // Save the existing bit pattern, but interpret it as an unsigned integer. 
            uint unum = BitConverter.ToUInt32(BitConverter.GetBytes(number), 0);
            ConsoleDisplay.ShowObject(unum, nameof(unum));

            //for (var i = 0; i < bytes.Length; i++)
            //{
            //    byte byt = bytes[i];
            //    var toBits = byt.ToBitsString();
            //    ConsoleDisplay.ShowObject(byt, nameof(byt));
            //    ConsoleDisplay.ShowObject(toBits, nameof(toBits));
            //}

            //var toBin = ToBin(number, length);
            //ConsoleDisplay.ShowObject(toBin, nameof(toBin));
            //var idx = toBin.LastIndexOf('0');
            //ConsoleDisplay.ShowObject(idx, nameof(idx));
            //var str3 = toBin.TrimStart('0');
            //var len = str3.Length;
            //var len2 = len % 4;
            //var len3 = 4 - len2;
            //var pad = len2 == 0 ? null : "0".PadRight(len3, '0');
            //ConsoleDisplay.ShowObject(pad, nameof(pad));
            //var result = pad + str3;
            ////result
            //ConsoleDisplay.ShowObject(result, nameof(result));
            //// Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')

        }

        internal void HashTest()
        {
            var number = 1234;
            var position = 2;
            var shifted = ShiftAndWrap(number, position);
            ConsoleDisplay.ShowObject(shifted, nameof(shifted));
        }

        public static string ToBin(int value, int len)
        {
            var str1 = len > 1 ? ToBin(value >> 1, len - 1) : null;
            var index = value & 1;
            var str4 = "01"[index];
            var str2 = str1 + "01"[value & 1];
            return str2;
            //return (len > 1 ?  ToBin(value >> 1, len - 1)  :  null) + "01"[value & 1];
        }

        private int ShiftAndWrap(int value, int positions)
        {
            positions = positions & 0x1F;

            // Save the existing bit pattern, but interpret it as an unsigned integer. 
            uint number = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);

            // Preserve the bits to be discarded. 
            uint wrapped = number >> (32 - positions);

            // Shift and wrap the discarded bits. 
            return BitConverter.ToInt32(BitConverter.GetBytes((number << positions) | wrapped), 0);
        }

    }
}
