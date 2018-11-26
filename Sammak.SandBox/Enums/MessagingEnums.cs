using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sammak.SandBox.Enums
{
    public class MessagingEnums
    {
        public enum EventType
        {
            Report = 103,
            Attachment = 119,
            Inventory = 122,
            Enrollment = 200,
        }

        public enum ReportEventSubTypes
        {
            Created = 104,
            QueueRetrieval = 105,
            Locked = 106,
            Unlocked = 107,
            DataDownloaded = 108,
            ScanStarted = 109,
            OnHold = 110,
            MillenniaDataCheckedOut = 111,
            MillenniaDataCheckedIn = 112,
            PdfPosted = 113,
            ReleaseHold = 114
        }

        public enum AttachmentEventSubTypes
        {
            Error = 1,
            UnfixableSubType = 121,
            RepairedSubType = 120
        }

        public enum InventoryEventSubTypes
        {
            Transfer = 123
        }

        public enum EnrollmentEventSubTypes
        {
            Submitted = 201,
            ServiceStarted = 202,
            AttachmentAdded = 203,
            OrderFulfilled = 204,
            ServiceCompleted = 205,
            EncounterAdded = 206
        }
    }
}
