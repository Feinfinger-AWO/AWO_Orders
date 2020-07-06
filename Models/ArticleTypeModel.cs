﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    /// <summary>
    /// Artikelart
    /// </summary>
    public class ArticleTypeModel :BaseModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string Ident { get; set; }
        [DisplayName("Bemerkung")]
        public string Note { get; set; }
    }
}
