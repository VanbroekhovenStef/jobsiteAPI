using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsite.DAL.Models
{
    public class Bedrijf
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Adres { get; set; }
        public string Omschrijving { get; set; }
        public string Foto { get; set; }
        public string Telefoon { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Vacature> Vacatures { get; set; }
    }
}
