using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    /// <summary>
    /// Basis Model für alle Entitäten
    /// </summary>
    public class BaseModel
    {
        [Newtonsoft.Json.JsonIgnore]
        [DisplayName("Zuletzt geändert")]
        public DateTime Changed { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [DisplayName("Von")]
        public int ChangedBy { get; set; }
        [ForeignKey("ChangedBy")]
        [Newtonsoft.Json.JsonIgnore]
        public virtual EmployeeModel Employee { get; set; }
    }
}
