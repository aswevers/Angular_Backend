using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProject.Models
{
    public class Gebruiker
    { 
        public long GebruikerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}
