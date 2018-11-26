using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sammak.SandBox.Inventory
{
    public interface IOrderEngine
    {
        void ProcessInitialOrder(Guid enrollmentId, string userName);
    }
}
