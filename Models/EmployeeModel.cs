using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    /// <summary>
    /// Model für Mitarbeiter
    /// </summary>
    public class EmployeeModel : BaseModel
    {
        private string password;

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
        private string passwordHash;
        [DisplayName("Passwort")]
        [Column("PasswordHash")]
        [Required]
        public string PasswordHash
        { 
            get 
            { 
                return passwordHash; 
            }
            set 
            { 
                passwordHash = value; 
            }
        }

        /// <summary>
        /// Erstellt Hashwert aus dem Passwort
        /// </summary>
        [NotMapped]
        public string Password 
        { 
            get 
            { 
                return password; 
            } 
            set 
            { 
                if (!string.IsNullOrWhiteSpace(value))
                {
                    password = value;
                    byte[] salt = new byte[128 / 8];
                    PasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA1,10000, 256 / 8));
                }
            } 
        }

    }
}
