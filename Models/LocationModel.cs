using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AWO_Orders.Models
{
    // Model der Niederlassung
    public class LocationModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Ident { get; set; }

        [DisplayName("Straße")]
        public string Street { get; set; }

        [DisplayName("Stadt")]
        public string City { get; set; }

        [DisplayName("Postleitzahl")]
        public string PLZ { get; set; }

        [DisplayName("Gebiet")]
        public string Area { get; set; }

        [DisplayName("Kostenstelle")]
        public string CostCenter { get; set; }
    }
}