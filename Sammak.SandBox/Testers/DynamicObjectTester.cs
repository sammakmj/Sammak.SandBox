using Newtonsoft.Json;
using Sammak.SandBox.Enums;
using Sammak.SandBox.Helpers;
using Sammak.SandBox.Models.EventModels;
using System;

namespace Sammak.SandBox.Testers
{
    public class DynamicObjectTester
    {
        public static void Run()
        {
            new DynamicObjectTester().EventMessageTest();
        }

        private void EventMessageTest()
        {
            var model1 = new Model1
            {
                Id = 5,
                Name = "Model1"
            };
            var model2 = new Model2
            {
                Id = Guid.NewGuid(),
                Name = "Model2"
            };


            var msg = MakeMessage(
                Guid.NewGuid(),
                MessagingEnums.EventType.Inventory.ToInt(),
                130,//order created  TODO: populate enum with meaningful values
                new { TimeStamp = DateTime.UtcNow, Bundle = model2 }

                );
            ShowObjectDyn(msg, nameof(msg));
        }

        private EventMessage MakeMessage(Guid objectGuid, int eventTypeId, int eventSubtypeId, dynamic details)
        {
            var msg = new EventMessage
            {
                ObjectGuid = objectGuid,
                EventTypeId = eventTypeId,
                EventSubtypeId = eventSubtypeId,
                Details = JsonConvert.SerializeObject(details)
            };
            return msg;
        }

        private void ObjectSerializerTest()
        {
            var model1 = new Model1
            {
                Id = 5,
                Name = "Model1"
            };
            ShowObjectDyn(model1, nameof(model1));
            //Console.WriteLine(JsonConvert.SerializeObject(model1, Formatting.Indented));

        }
        public static void ShowObject(object instance, string nameOfInstance)
        {
            Console.WriteLine($"{nameOfInstance}:");
            Console.WriteLine(JsonConvert.SerializeObject(instance, Formatting.Indented));
        }

        public static void ShowObjectDyn(dynamic instance, string nameOfInstance)
        {
            Console.WriteLine($"{nameOfInstance}:");
            Console.WriteLine(JsonConvert.SerializeObject(instance, Formatting.Indented));
        }

    }

    public class Model1
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Model2
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}
