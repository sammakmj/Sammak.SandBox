using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sammak.SandBox.Models.EventModels
{
    public interface IEventMessage
    {
        int EventTypeId { get; set; }
        int? EventSubtypeId { get; set; }
        int? ObjectType { get; set; }
        int? ObjectId { get; set; }
        Guid? ObjectGuid { get; set; }
        string Details { get; set; }
    }
}
