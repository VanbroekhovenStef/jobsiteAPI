using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsite.DAL.Models
{
    public class Vacature
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public int BedrijfId { get; set; }
        public Bedrijf Bedrijf { get; set; }
        public string Omschrijving { get; set; }
        public string Kwalificaties { get; set; }
        public DateTime DatumSluiting { get; set; }
        public ICollection<Sollicitatie> Sollicitaties { get; set; }
    }
}
