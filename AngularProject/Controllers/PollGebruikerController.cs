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
    public class PollGebruikerController : ControllerBase
    {
        private readonly ProjectContext _context;

        public PollGebruikerController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/PollGebruiker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PollGebruiker>>> GetPollGebruikers()
        {
            return await _context.PollGebruikers.Include(p => p.Poll).Include(p=>p.Gebruiker).ToListAsync();
        }

        // GET: api/PollGebruiker
        // Haalt alle pollGebruiker objecten op waar de gebruikerId == gebruikerId
        [Authorize]
        [HttpGet]
        [Route("getPGWhereGebruiker/{gebruikerId}")]
        public async Task<ActionResult<IEnumerable<PollGebruiker>>> GetPollGebruikersWhereGebruikerId(long gebruikerId)
        {
            return await _context.PollGebruikers.Include(p => p.Poll).Include(p => p.Gebruiker).Where(p => p.GebruikerId == gebruikerId).ToListAsync();
        }

        // GET: api/PollGebruiker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PollGebruiker>> GetPollGebruiker(long id)
        {
            var pollGebruiker = _context.PollGebruikers.Include(p => p.Poll).Include(p => p.Gebruiker).FirstOrDefault(k => k.PollGebruikerId == id);

            if (pollGebruiker == null)
            {
                return NotFound();
            }

            return pollGebruiker;
        }

        // PUT: api/PollGebruiker/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPollGebruiker(long id, PollGebruiker pollGebruiker)
        {
            if (id != pollGebruiker.PollGebruikerId)
            {
                return BadRequest();
            }

            _context.Entry(pollGebruiker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollGebruikerExists(id))
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

        // POST: api/PollGebruiker
        [HttpPost]
        public async Task<ActionResult<PollGebruiker>> PostPollGebruiker(PollGebruiker pollGebruiker)
        {
            _context.PollGebruikers.Add(pollGebruiker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPollGebruiker", new { id = pollGebruiker.PollGebruikerId }, pollGebruiker);
        }

        // DELETE: api/PollGebruiker/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PollGebruiker>> DeletePollGebruiker(long id)
        {
            var pollGebruiker = await _context.PollGebruikers.FindAsync(id);
            if (pollGebruiker == null)
            {
                return NotFound();
            }

            _context.PollGebruikers.Remove(pollGebruiker);
            await _context.SaveChangesAsync();

            return pollGebruiker;
        }

        private bool PollGebruikerExists(long id)
        {
            return _context.PollGebruikers.Any(e => e.PollGebruikerId == id);
        }
    }
}
