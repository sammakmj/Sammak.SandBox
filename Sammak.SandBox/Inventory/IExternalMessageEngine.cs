using Sammak.SandBox.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sammak.SandBox.Inventory
{
    public interface IExternalMessageEngine
    {
        void Process(ExternalMessagePayload msg);

        void ProcessWithComparables(ComparableExternalMessagePayload payload);
    }
}
