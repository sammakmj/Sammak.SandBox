using System;

namespace Sammak.SandBox.DependencyManagement
{
    public class InterfaceImplementerPair
    {
        public Type InterfaceType { get; set; }
        public Type ImplementerType { get; set; }

        public string Test => "mjs";

    }
}
