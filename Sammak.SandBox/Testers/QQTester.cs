using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Sammak.SandBox.Helpers;
using Sammak.SandBox.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sammak.SandBox.Testers
{
    public class QQTester
    {
         public static IConfigurationRoot ConfigurationRoot;

        public static void Run()
        {
            new QQTester().QQTest();
        }

        private void QQTest()
        {
            var device = new Device
            {
                Id = new Guid(),
                Name = "MJS"
            };
            var kitDetail = new KitDetail
            {
                Device = device
            };

            ConsoleDisplay.ShowObject(kitDetail, nameof(kitDetail));
        }

    }
    public class Device
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? DeviceTypeId { get; set; }
    }

    #region helper classes
    public class KitDetail
    {
        private string _inventoryDescription;

        public Device Device { get; set; }

        public Guid Id { get; set; }
        public string InventoryDescription
        {
            get
            {
                if (string.IsNullOrEmpty(_inventoryDescription) && Device != null && Device.Name != null)
                {
                    _inventoryDescription = Device.Name;
                }
                return _inventoryDescription;
            }
            set => _inventoryDescription = value;
        }
    }

    #endregion
}
