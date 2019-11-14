using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProject.Models
{
    public class Vriend
    {

        public long Id { get; set; }
        public long GebruikerId1 { get; set; }
        public long GebruikerId2 { get; set; }
        public Boolean Geaccepteerd { get; set; }

        public Gebruiker Gebruiker1 { get; set; }
        public Gebruiker Gebruiker2 { get; set; }
    }
}
