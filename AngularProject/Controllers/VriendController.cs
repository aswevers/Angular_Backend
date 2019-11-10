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

namespace AngularProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VriendController : ControllerBase
    {
        private readonly ProjectContext _context;

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

        [Authorize]
        [HttpGet]
        [Route("getPendingFriendRequests/{gebruikerId}")]
        public async Task<ActionResult<IEnumerable<Vriend>>> GetPendingFriendRequests(long gebruikerId)
        {
            return await _context.Vrienden.Include(v => v.Gebruiker1).Include(v => v.Gebruiker2)
                .Where(v=>v.Gebruiker1.GebruikerId == gebruikerId || v.Gebruiker2.GebruikerId == gebruikerId)
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
            _context.Vrienden.Add(vriend);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVriend", new { id = vriend.Id }, vriend);
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
    }
}
