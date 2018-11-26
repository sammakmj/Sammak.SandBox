using System.ComponentModel;

namespace Sammak.SandBox.Models
{
    public class ActionResultBase
    {
        public enum ActionResultType
        {
            /// <summary>
            /// value of 0 on all result enumeration indicate operation Success
            /// </summary>
            [Description("The operation is successful")]
            Success,

            /// <summary>
            /// value of 1 on all result enumeration indicate operation failure
            /// </summary>
            [Description("The operation has failed")]
            Failed,  // This case will cover any other case different from the following list
        }

        public int ResultType { get; set; }

        public virtual string ResultTypeDescription
        {
            get { return ResultType == 0 ? "Success" : ResultType.ToString(); }
            protected set { ResultTypeDescription = value; }
        }

        public string ValidationMessage { get; set; } = "";
    }
}
