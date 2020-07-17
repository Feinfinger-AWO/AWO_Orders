using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    public class OrderPositionModel :BaseModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Beschreibung")]
        public string Description { get; set; }

        [DisplayName("Artikelnummer")]
        public string ArticleNumber { get; set; }

        [DisplayName("Menge")]
        public decimal Count { get; set; }

        [DisplayName("Auftrag")]
        public int OrderId { get; set; }
        public virtual OrderModel Order { get; set; }

        [DisplayName("Typ")]
        public int ArticleTypeId { get; set; }
        public virtual ArticleTypeModel ArticleType { get; set; }

        [DisplayName("Nummer")]
        public int Number { get; set; }
    }
}
