using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    // Model der Niederlassung
    public class LocationModel :BaseModel
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


    }
}
