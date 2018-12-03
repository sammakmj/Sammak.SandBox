using Sammak.SandBox.Models;

namespace Sammak.SandBox.Inventory.ExtenderWrapper
{
    public interface IExternalMessageEngineStaticWrapper
    {
        void Process(IExternalMessageEngine externalMessageEngine, ExternalMessagePayload payload);
   }
}
