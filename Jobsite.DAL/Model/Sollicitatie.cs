using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsite.DAL.Models
{
    public class Sollicitatie
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VacatureId { get; set; }
        public User User { get; set; }

        [ForeignKey("VacatureId")]
        public Vacature Vacature { get; set; }
        public string Motivatie { get; set; }
    }
}
