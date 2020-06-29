using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    /// <summary>
    /// Model für das Zugriffsrecht
    /// </summary>
    public class RightModel
    {
        public int Id { get; set; }

        [Required]
        public string Ident { get; set; }
        public bool CanView { get; set; }
        public bool CanOrder { get; set; }
        public bool CanProcess { get; set; }
        public DateTime Changed { get; set; }
        public int ChangedBy { get; set; }
    }
}
