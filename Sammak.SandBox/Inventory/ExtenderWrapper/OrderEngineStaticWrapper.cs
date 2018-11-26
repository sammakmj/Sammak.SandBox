using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sammak.SandBox.Models;

namespace Sammak.SandBox.Inventory.ExtenderWrapper
{
    public class OrderEngineStaticWrapper : IOrderEngineStaticWrapper
    {
        public void HandleExternalMessage(IOrderEngine orderEngine, ExternalMessagePayload payload)
        {
            orderEngine.HandleExternalMessage(payload);
        }
    }
}
