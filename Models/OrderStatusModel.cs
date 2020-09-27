using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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