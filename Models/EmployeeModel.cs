using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    /// <summary>
    /// Model für Mitarbeiter
    /// </summary>
    public class EmployeeModel
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required]
        public string SureName { get; set; }

        [DisplayName("Vorname")]
        public string Forename { get; set; }
        
        [DisplayName("Beruf")]
        public string Job { get; set; }
        
        [DisplayName("Telefon")]
        public string Telephone { get; set; }

        [DisplayName("eMail")]
        [Required]
        public string EMail { get; set; }

        [DisplayName("Manager")]
        public bool IsManager { get; set; }

        [DisplayName("Zugangsrecht")]
        [Required]
        public int RightId { get; set; }
        public virtual RightModel Right { get; set; }

        [Column("LocId")]
        [DisplayName("Niederlassung")]
        [Required]
        public virtual int LocationId { get; set; }
        public LocationModel Location { get; set; }

        public string PasswordHash { get; set; }

        [DisplayName("Geändert")]
        public DateTime Changed { get; set; }

        [DisplayName("Von")]
        public int ChangedBy { get; set; }
        public EmployeeModel Employee { get; set; }

    }
}
