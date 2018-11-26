using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sammak.SandBox.Models.Entity
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }

    }
}
