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
    public class StemController : ControllerBase
    {
        private readonly ProjectContext _context;

        public StemController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Stem
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stem>>> GetStemmen()
        {
            return await _context.Stemmen.Include(s=> s.Keuze).Include(s => s.Gebruiker).ToListAsync();
        }

        // GET: api/Stem/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Stem>> GetStem(long id)
        {
            var stem = _context.Stemmen.Include(s => s.Keuze).Include(s => s.Gebruiker).FirstOrDefault(s => s.StemId == id);

            if (stem == null)
            {
                return NotFound();
            }

            return stem;
        }

        // GET: api/Stem/5
        [Authorize]
        [HttpGet]
        [Route("getStemmenByPollId/{pollId}")]
        public async Task<ActionResult<IEnumerable<Stem>>> GetStemmenByPollId(long pollId)
        {
            var stemmen = await _context.Stemmen.Include(s => s.Keuze).Include(s => s.Gebruiker).Where(s => s.Keuze.PollId == pollId).ToListAsync();

            if (stemmen == null)
            {
                return NotFound();
            }

            return stemmen;
        }

        // PUT: api/Stem/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStem(long id, Stem stem)
        {
            if (id != stem.StemId)
            {
                return BadRequest();
            }

            _context.Entry(stem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StemExists(id))
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

        // POST: api/Stem
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Stem>> PostStem(Stem stem)
        {
            _context.Stemmen.Add(stem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStem", new { id = stem.StemId }, stem);
        }

        // DELETE: api/Stem/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Stem>> DeleteStem(long id)
        {
            var stem = await _context.Stemmen.FindAsync(id);
            if (stem == null)
            {
                return NotFound();
            }

            _context.Stemmen.Remove(stem);
            await _context.SaveChangesAsync();

            return stem;
        }

        private bool StemExists(long id)
        {
            return _context.Stemmen.Any(e => e.StemId == id);
        }
    }
}
