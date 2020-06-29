using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    /// <summary>
    /// Artikelart
    /// </summary>
    public class ArticleTypeModel
    {
        public int Id { get; set; }
        [Required]
        public string Ident { get; set; }
        public string Note { get; set; }
        public DateTime Changed { get; set; }
        public int ChangedBy { get; set; }
    }
}
