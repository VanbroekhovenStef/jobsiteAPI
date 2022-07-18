using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsite.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public string? Adres { get; set; }
        public string? Telefoon { get; set; }
        public string? CV { get; set; }
        public string? LinkedIn { get; set; }
        public int RoleId { get; set; }
        ICollection<Bedrijf> Bedrijven { get; set; }
        ICollection<Sollicitatie> Sollicitaties { get; set; }
        [NotMapped]
        public string Token { get; set; }

    }
}
