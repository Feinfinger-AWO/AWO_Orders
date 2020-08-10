using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    /// <summary>
    /// Auftragsstatus
    /// </summary>
    public class OrderStatusModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Ident { get; set; }

        [DisplayName("Bemerkung")]
        public string Note { get; set; }

        [DisplayName("Basistyp")]
        public OrderBaseStatusEnum BaseStatus { get; set; }

        [DisplayName("Sortierung")]
        public int SortNumber { get; set; }
    }
}
