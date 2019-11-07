using AngularProject.Data;
using AngularProject.Helpers;
using AngularProject.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AngularProject.Services
{
    public class GebruikerService : IGebruikerService
    {
        private readonly AppSettings _appSettings;
        private readonly ProjectContext _projectContext;
        public GebruikerService(IOptions<AppSettings> appSettings, ProjectContext projectContext)
        {
            _appSettings = appSettings.Value;
            _projectContext = projectContext;
        }
        public Gebruiker Authenticate(string email, string password)
        {
            var user = _projectContext.Gebruikers.SingleOrDefault(x => x.Email == email && x.Password == password);
            // return null if user not found
            if (user == null)
            {
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim("GebruikerId", user.GebruikerId.ToString()),
                    new Claim("Email", user.Email),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            // remove password before returning
            user.Password = null;
            return user;
        }
    }
}
