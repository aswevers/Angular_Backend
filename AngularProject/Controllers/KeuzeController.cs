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
    public class KeuzeController : ControllerBase
    {
        private readonly ProjectContext _context;

        public KeuzeController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Keuze
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Keuze>>> GetKeuzes()
        {
            return await _context.Keuzes.Include(k => k.Poll).ToListAsync();
        }

        // GET: api/Keuze/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Keuze>> GetKeuze(long id)
        {
            var keuze = _context.Keuzes.Include(k => k.Poll).FirstOrDefault(k => k.KeuzeId == id);

            if (keuze == null)
            {
                return NotFound();
            }

            return keuze;
        }

        // Haalt alle keuzes op waar pollId = pollId
        [Authorize]
        [HttpGet]
        [Route("getKeuzesByPollId/{pollId}")]
        public async Task<ActionResult<IEnumerable<Keuze>>> GetKeuzesByPollId(long pollId)
        {
            var keuzes = await _context.Keuzes.Where(k => k.PollId == pollId).Include(k => k.Poll).ToListAsync();

            if (keuzes == null)
            {
                return NotFound();
            }

            return keuzes;
        }

        // PUT: api/Keuze/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKeuze(long id, Keuze keuze)
        {
            if (id != keuze.KeuzeId)
            {
                return BadRequest();
            }

            _context.Entry(keuze).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeuzeExists(id))
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

        // POST: api/Keuze
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Keuze>> PostKeuze(Keuze keuze)
        {
            _context.Keuzes.Add(keuze);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKeuze", new { id = keuze.KeuzeId }, keuze);
        }

        // DELETE: api/Keuze/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Keuze>> DeleteKeuze(long id)
        {
            var keuze = await _context.Keuzes.FindAsync(id);
            if (keuze == null)
            {
                return NotFound();
            }

            _context.Keuzes.Remove(keuze);
            await _context.SaveChangesAsync();

            return keuze;
        }

        private bool KeuzeExists(long id)
        {
            return _context.Keuzes.Any(e => e.KeuzeId == id);
        }
    }
}
