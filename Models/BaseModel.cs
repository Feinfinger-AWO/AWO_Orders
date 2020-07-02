using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    /// <summary>
    /// Basis Model für alle Entitäten
    /// </summary>
    public class BaseModel
    {
        [DisplayName("Zuletzt geändert")]
        public DateTime Changed { get; set; }

        [DisplayName("Von")]
        public int ChangedBy { get; set; }
        [ForeignKey("ChangedBy")]
        public virtual EmployeeModel Employee { get; set; }
    }
}
