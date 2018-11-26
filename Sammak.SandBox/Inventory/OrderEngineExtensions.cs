using Sammak.SandBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sammak.SandBox.Inventory
{
    #region Temporary extension to the interface to test 

    public static class OrderEngineExtensions
    {
        public static void HandleExternalMessage(this IOrderEngine orderEngine, ExternalMessagePayload payload)
        {
            // TODO: some temporary implementation
        }
    }

    #endregion
}
