using System;
using System.Collections.Generic;
using System.Text;

namespace Sammak.SandBox.OptionalArgs
{
    interface ISearch
    {
        string SearchWithOptionalArgs(string name = null, bool? inActive = null);
    }

    public class Search : ISearch
    {
        public string SearchWithOptionalArgs(string name, bool? inActive)
        {
            var inActiveText = inActive.HasValue ? inActive.ToString() : "Null";
            var msg = $"Name = {name ?? "Null"}; InActive = {inActiveText}";
            return msg;
        }
    }
}
