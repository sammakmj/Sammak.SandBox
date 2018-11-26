using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sammak.SandBox.Models
{
    public static class ExternalMessageType
    {
        public const int CreateOrder = 1;
        public const int FulfillOrder = 2;
        public const int CancelOrder = 3;
        public const int OrderCreated = 4;
        public const int OrderFulfilled = 5;
        public const int OrderCancelled = 6;
    }
}
