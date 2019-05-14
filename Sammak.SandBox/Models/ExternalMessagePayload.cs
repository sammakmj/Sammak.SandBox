using System;
using System.Collections.Generic;

namespace Sammak.SandBox.Models
{

    public class ExternalMessagePayload
    {
        #region Properties

        public int MessageType { get; set; }

        public string OrderId { get; set; }

        public int SourceSystemId { get; set; }

        public Dictionary<string, string> ParameterDictionary { get; set; }

        public string UserName { get; set; }
        #endregion

    }
}
