using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        public virtual OrderStatusModel Status { get; set; }

        [DisplayName("Mitarbeiter")]
        public int EmplId { get; set; }

        public virtual EmployeeModel Empl { get; set; }

        [DisplayName("Bemerkung")]
        public string Note { get; set; }
    }
}