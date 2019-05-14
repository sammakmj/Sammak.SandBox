using System;
using System.Collections.Generic;
using System.Text;

namespace Sammak.SandBox.Models
{
    public class NovusExternalMessage
    {
        public decimal MessageType { get; set; }
        public int SourceSystemId { get; set; }
        public string OrderId { get; set; }
        public string UserName { get; set; }
    }
}
