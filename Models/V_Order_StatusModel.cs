using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    /// <summary>
    /// Übersicht der Bestellung anch Anzahl und Status
    /// </summary>
    public class V_Order_StatusModel
    {
        public int Id { get; set; }
        [DisplayName("Anzahl")]
        public int Count { get; set; }
        [DisplayName("Status")]
        public string Status { get; set; }
        public int SortNumber { get; set; }
    }
}
