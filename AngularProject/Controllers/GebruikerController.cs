using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularProject.Data;
using AngularProject.Models;
using AngularProject.Services;
using Microsoft.AspNetCore.Authorization;

namespace AngularProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly IGebruikerService _gebruikerService;
        public Gebruiker gebruikerSendRequest;
        public Gebruiker gebruikerReceiveRequest;
        public GebruikerController(IGebruikerService gebruikerService, ProjectContext projectContext)
        {
            _context = projectContext;
            _gebruikerService = gebruikerService;
        }
        

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Gebruiker userParam)
        {
            var user = _gebruikerService.Authenticate(userParam.Email, userParam.Password);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(user);
        }



        // GET: api/Gebruiker
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gebruiker>>> GetGebruikers()
        {
            var userID = User.Claims.FirstOrDefault(c => c.Type == "GebruikerId").Value;
            return await _context.Gebruikers.ToListAsync();
        }
        // GET: api/Gebruiker/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Gebruiker>> GetGebruiker(long id)
        {
            var gebruiker = await _context.Gebruikers.FindAsync(id);

            if (gebruiker == null)
            {
                return NotFound();
            }
            gebruikerSendRequest = gebruiker;
            return gebruiker;
        }

        // GET: api/Gebruiker/5
        // Haalt een gebruiker met Email == email op, indien deze niet bestaat wordt er een gebruiker met een tijdelijk wachtwoord aangemaakt
        [Authorize]
        [HttpGet]
        [Route("getByEmail/{email}")]
        public async Task<ActionResult<Gebruiker>> GetGebruikerByEmail(string email)
        {
            var gebruiker =  _context.Gebruikers.Where(g => g.Email == email).FirstOrDefault();

            if (gebruiker == null)
            {
                Gebruiker gebruikertemp = new Gebruiker { Email = email, Password = "temp123" };
                _context.Gebruikers.Add(gebruikertemp);
                await _context.SaveChangesAsync();
                gebruikerReceiveRequest = gebruikertemp;
                return CreatedAtAction("GetGebruiker", new { id = gebruikertemp.GebruikerId }, gebruikertemp);
            }
            else
            {
                gebruikerReceiveRequest = gebruiker;
                return gebruiker;
            }
        }

        // PUT: api/Gebruiker/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGebruiker(long id, Gebruiker gebruiker)
        {
            if (id != gebruiker.GebruikerId)
            {
                return BadRequest();
            }

            _context.Entry(gebruiker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GebruikerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Gebruiker
        [HttpPost]
        public async Task<ActionResult<Gebruiker>> PostGebruiker(Gebruiker gebruiker)
        {
            _context.Gebruikers.Add(gebruiker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGebruiker", new { id = gebruiker.GebruikerId }, gebruiker);
        }


        // DELETE: api/Gebruiker/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gebruiker>> DeleteGebruiker(long id)
        {
            var gebruiker = await _context.Gebruikers.FindAsync(id);
            if (gebruiker == null)
            {
                return NotFound();
            }

            _context.Gebruikers.Remove(gebruiker);
            await _context.SaveChangesAsync();

            return gebruiker;
        }

        private bool GebruikerExists(long id)
        {
            return _context.Gebruikers.Any(e => e.GebruikerId == id);
        }
    }
}
