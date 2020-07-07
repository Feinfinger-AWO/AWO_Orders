using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    /// <summary>
    /// Bestellung
    /// </summary>
    public class OrderModel : BaseModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Nummer")]
        public string Number { get; set; }
        [DisplayName("Erstellt")]
        public DateTime PlaceDate { get; set; }
        [DisplayName("Status")]
        public int StatusId { get; set; }
        [DisplayName("Mitarbeiter")]
        public int EmplId { get; set; }
        [DisplayName("Bemerkung")]
        public string Note { get; set; }
    }
}
