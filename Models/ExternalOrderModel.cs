using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AWO_Orders.Models
{
    /// <summary>
    /// Externe Bestellung
    /// </summary>
    public class ExternalOrderModel : BaseModel
    {
        [DisplayName("Nummer")]
        public long Id { get; set; }

        [Required]
        [DisplayName("Manager")]
        public int ManagerId { get; set; }

        public virtual EmployeeModel Manager { get; set; }

        [Required]
        [DisplayName("Erstellt am")]
        public DateTime ProcessedAt { get; set; }

        [DisplayName("Bemerkung")]
        public string Notes { get; set; }

        [DisplayName("Dokument")]
        public byte[] Document { get; set; }
    }
}