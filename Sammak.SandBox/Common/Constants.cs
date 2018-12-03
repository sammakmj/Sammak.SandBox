using System;
using System.Collections.Generic;
using System.Text;

namespace Sammak.SandBox.Common
{
    public class Constants
    {
        public class QueueNames
        {
            private static readonly string thisDomainName = "Inventory";

            public static string ExternalMessage = $"{thisDomainName}.{nameof(ExternalMessage)}";
            public static string ProcessInitialOrder = $"{thisDomainName}.{nameof(ProcessInitialOrder)}";
            public static string AddMTPOrder = $"{thisDomainName}.{nameof(AddMTPOrder)}";
            public static string SetBundleToAssignedToPatientPending = $"{thisDomainName}.{nameof(SetBundleToAssignedToPatientPending)}";
            public static string HandleBundleActivated = $"{thisDomainName}.{nameof(HandleBundleActivated)}";
        }
    }
}
