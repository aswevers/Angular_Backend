using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProject.Models
{
    public class Keuze
    {
        public long KeuzeId { get; set; }
        public string Naam { get; set; }
        public long PollId { get; set; }

        public Poll Poll { get; set; }

    }
}
