using AngularProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProject.Services
{
    public interface IGebruikerService
    {
        Gebruiker Authenticate(string email, string password);
    }
}
