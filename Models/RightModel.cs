using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AWO_Orders.Models
{
    /// <summary>
    /// Model für das Zugriffsrecht
    /// </summary>
    public class RightModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Ident { get; set; }

        [DisplayName("Ansicht")]
        public bool CanView { get; set; }

        [DisplayName("Erstellen")]
        public bool CanOrder { get; set; }

        [DisplayName("Abarbeitung")]
        public bool CanProcess { get; set; }
    }
}