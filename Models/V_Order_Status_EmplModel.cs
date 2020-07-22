using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    /// <summary>
    /// Übersicht der Bestellung nach Mitarbeiter, Anzahl und Status
    /// </summary>
    public class V_Order_Status_EmplModel
    {
        public int Id { get; set; }
        [DisplayName("Anzahl")]
        public int Count { get; set; }
        [DisplayName("Status")]
        public string Status { get; set; }
        [DisplayName("Mitarbeiter")]
        public int EmplId { get; set; }
        public virtual EmployeeModel Empl { get; set; }
    }
}
