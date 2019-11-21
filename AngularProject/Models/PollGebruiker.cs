using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProject.Models
{
    public class PollGebruiker
    {
        public long PollGebruikerId { get; set; }
        public long GebruikerId { get; set; }
        public long PollId { get; set; }
        public Boolean HeeftAangemaakt { get; set; }
        public Boolean HeeftGeaccepteerd { get; set; }

        public Gebruiker Gebruiker { get; set; }
        public Poll Poll { get; set; }
    }
}
