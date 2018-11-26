using Sammak.SandBox.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sammak.SandBox.Models.EventModels
{
    public class EventType : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public virtual EventType Parent { get; set; }
    }
}
