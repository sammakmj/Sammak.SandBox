using Sammak.SandBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sammak.SandBox.Inventory.ExtenderWrapper
{
    public interface IOrderEngineStaticWrapper
    {
        void HandleExternalMessage(IOrderEngine orderEngine, ExternalMessagePayload payload);
   }
}
