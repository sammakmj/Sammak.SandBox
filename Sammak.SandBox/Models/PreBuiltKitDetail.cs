using Sammak.SandBox.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sammak.SandBox.Models
{
    public class PreBuiltKitDetail
    {
        private static string CardioKeyPartNumber;
        private static string CardioKeyDeviceTypeId;
        private static List<string> ePatchPartNumbers = new List<string>();

        public Guid? DeviceTypeId { get; set; }
        public string PartNumber { get; set; }
        public string SerialNumber { get; set; }
        public string ReturnTrackingNumbe { get; set; }
        public bool RequiresReturnTrackingNumber
        {
            get
            {
                if(string.IsNullOrEmpty(PartNumber))
                    return false;
                if (PartNumber.Equals(CardioKeyPartNumber, StringComparison.CurrentCultureIgnoreCase))
                    return true;

                if (ePatchPartNumbers.Contains(PartNumber.ToLower()))
                    return true;

                return false;
            }
        }

        static PreBuiltKitDetail()
        {
            CardioKeyPartNumber = ApplicationHelper.GetAppSettingValue("CardioKeyPartNumber");
            CardioKeyDeviceTypeId = ApplicationHelper.GetAppSettingValue("CardioKeyDeviceTypeId");
            var ePatchPartNumberStrings = ApplicationHelper.GetAppSettingValue("ePatchPartNumber");
            var ePatchPartNumberStringArray = ePatchPartNumberStrings.Split(',');
                
            for(var idx = 0; idx <= ePatchPartNumberStringArray.Length; idx++)
            {
                if(idx != 1 && idx != 2)
                {
                    ePatchPartNumbers.Add(ePatchPartNumberStringArray[idx].ToLower());
                }
            }
        }
    }

}
