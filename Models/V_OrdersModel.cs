using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    public class V_OrdersModel
    {
        public int Id { get; set; }
        [DisplayName("Vom")]
        public DateTime PlaceDate { get; set; }
        [DisplayName("StatusId")]
        public int StatusId { get; set; }
        [DisplayName("Auftrag")]
        public string Number { get; set; }
        [DisplayName("Status")]
        public string Status { get; set; }
        [DisplayName("Mitarbeiter")]
        public string Employee { get; set; }
        [Key]
        [DisplayName("PosId")]
        public int PosId { get; set; }
        [DisplayName("Beschreibung")]
        public string Description { get; set; }
        [DisplayName("Artikelnummer")]
        public string ArticleNumber { get; set; }
        [DisplayName("Artikelart")]
        public string ArticleType { get; set; }
        [DisplayName("Position")]
        public int PosNumber { get; set; }
        [DisplayName("Anzahl")]
        public decimal Count { get; set; }
        [DisplayName("Pos. Status")]
        public PositionStatusEnum PosStatus { get; set; }
        [DisplayName("BaseStatus")]
        public OrderBaseStatusEnum BaseStatus { get; set; }

        public int LocId { get; set; }
        [DisplayName("Niederlassung")]
        public string Location { get; set; }
        [DisplayName("Gebiet")]
        public string Area { get; set; }
        [DisplayName("Kostenstelle")]
        public string CostCenter { get; set; }

        [NotMapped]
        [DisplayName("Ausgewählt")]
        public bool Selected { get; set; }
        [NotMapped]
        [DisplayName("Abgelehnt")]
        public bool Rejected { get; set; }
    }
}
