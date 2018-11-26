using Sammak.SandBox.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sammak.SandBox.Models.EventModels
{
    public class EventMessage : EntityBase, IEventMessage
    {
        public int EventTypeId { get; set; }
        public virtual EventType EventType { get; set; }
        public int? EventSubtypeId { get; set; }
        public virtual EventType EventSubtype { get; set; }
        public int? ObjectType { get; set; }
        public int? ObjectId { get; set; }
        public Guid? ObjectGuid { get; set; }
        public string Details { get; set; }
    }
}
