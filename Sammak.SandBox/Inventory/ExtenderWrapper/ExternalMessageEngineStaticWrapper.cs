using Sammak.SandBox.Models;

namespace Sammak.SandBox.Inventory.ExtenderWrapper
{
    public class ExternalMessageEngineStaticWrapper : IExternalMessageEngineStaticWrapper
    {
        public void HandleExternalMessage(IExternalMessageEngine orderEngine, ExternalMessagePayload payload)
        {
        }

        public void Process(IExternalMessageEngine externalMessageEngine, ExternalMessagePayload payload)
        {
            externalMessageEngine.Process(payload);
        }
    }
}
