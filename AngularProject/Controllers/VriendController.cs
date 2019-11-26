using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularProject.Data;
using AngularProject.Models;
using Microsoft.AspNetCore.Authorization;
using AngularProject.Services;
using System.Net;
using System.Net.Mail;

namespace AngularProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VriendController : ControllerBase
    {
        private readonly ProjectContext _context;
        private IGebruikerService gebruikerService;

        public VriendController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Vriend
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vriend>>> GetVrienden()
        {
            return await _context.Vrienden.Include(v => v.Gebruiker1).Include(v => v.Gebruiker2).ToListAsync();
        }

        // GET: api/Gebruiker
        // Haalt vriendrelaties op waar de gebruiker met gebruikerId = id deel van uitmaakt.
        [Authorize]
        [HttpGet]
        [Route("getVriendenWhereGebruikerId/{id}")]
        public async Task<ActionResult<IEnumerable<Vriend>>> GetVriendenWhereGebruikerId(long id)
        {
            return await _context.Vrienden.Include(v => v.Gebruiker1)
                .Include(v => v.Gebruiker2)
                .Where(g => g.Gebruiker1.GebruikerId == id || g.Gebruiker2.GebruikerId == id).Where(g => g.Geaccepteerd == true).ToListAsync();
        }

        // GET: api/Gebruiker
        //Haalt vriendrelaties op waar gebruiker met gebruikerId = id deel van uitmaakt en Geaccepteerd == false

        [Authorize]
        [HttpGet]
        [Route("getPendingFriendRequests/{gebruikerId}")]
        public async Task<ActionResult<IEnumerable<Vriend>>> GetPendingFriendRequests(long gebruikerId)
        {
            return await _context.Vrienden.Include(v => v.Gebruiker1).Include(v => v.Gebruiker2)
                .Where(v=>v.Gebruiker1.GebruikerId == gebruikerId)
                .Where(v => v.Geaccepteerd == false).ToListAsync();
        }

        // GET: api/Vriend/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Vriend>> GetVriend(long id)
        {
            var vriend = await _context.Vrienden.FindAsync(id);

            if (vriend == null)
            {
                return NotFound();
            }

            return vriend;
        }

        // PUT: api/Vriend/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVriend(long id, Vriend vriend)
        {
            if (id != vriend.Id)
            {
                return BadRequest();
            }

            _context.Entry(vriend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VriendExists(id))
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

        // POST: api/Vriend
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Vriend>> PostVriend(Vriend vriend)
        {
            _context.Entry(vriend.Gebruiker1).State = EntityState.Modified;
            _context.Entry(vriend.Gebruiker2).State = EntityState.Modified;

            _context.Vrienden.Add(vriend);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVriend", new { id = vriend.Id }, vriend);
        }

        // Maakt vriendrelatie met Geaccepteerd == false
        [Authorize]
        [HttpPost]
        [Route("sendRequest")]
        public async Task<ActionResult<Vriend>> SendRequest()
        {
            GebruikerController gebruikerController = new GebruikerController(gebruikerService, _context);
            Vriend newVriend = new Vriend { Gebruiker1= gebruikerController.gebruikerReceiveRequest, Gebruiker2=gebruikerController.gebruikerSendRequest, Geaccepteerd=false };
            _context.Vrienden.Add(newVriend);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVriend", new { id = newVriend.Id }, newVriend);
            
        }

        // DELETE: api/Vriend/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vriend>> DeleteVriend(long id)
        {
            var vriend = await _context.Vrienden.FindAsync(id);
            if (vriend == null)
            {
                return NotFound();
            }

            _context.Vrienden.Remove(vriend);
            await _context.SaveChangesAsync();

            return vriend;
        }

        private bool VriendExists(long id)
        {
            return _context.Vrienden.Any(e => e.Id == id);
        }

        //Verzend email
        [Authorize]
        [HttpGet]
        [Route("sendMail/{email}")]
        public void SendMail(string email)
        {
            MailAddress to = new MailAddress(email);
            MailAddress from = new MailAddress("noreply@northpoll.com");

            MailMessage message = new MailMessage(from, to);
            message.Subject = "Uitnodiging voor een nieuwe poll!";
            message.Body = "Hallo " + email + ", \n Je bent uitgenodigd voor een nieuwe poll op North Poll! \n\n" 
                + "Om deel te kunnen nemen aan deze poll volg je de volgende stappen: \n\n"
                + "1. Ga naar onze site \n2. Klik op 'Log In'\n Log je in met deze gegevens: \n  -Email: " + email + "\n  -Wachtwoord: temp123\n"
                + "4. (Optioneel) Verander je wachtwoord door op 'Wachtwoord veranderen' te klikken\n"
                + "5. Ga naar 'Vriendschapsverzoeken' en accepteer het vriendschapsverzoek van de vriend die je uitgenodigt heeft\n"
                + "6. Ga naar 'Mijn polls' en stem op de poll!\n\n"
                + "Veel pollplezier!";

            SmtpClient client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("55c26ab6e5377c", "7de79150a5f454"),
                EnableSsl = true
            };
            // code in brackets above needed if authentication required 

            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
