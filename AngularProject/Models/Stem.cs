using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProject.Models
{
    public class Stem
    {
        public long StemId { get; set; }

        public long KeuzeId { get; set; }
        public long GebruikerId { get; set; }

        public Keuze Keuze { get; set; }
        public Gebruiker Gebruiker { get; set; }
    }
}
