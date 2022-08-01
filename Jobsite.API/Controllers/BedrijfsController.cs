using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Jobsite.DAL.Data;
using Jobsite.DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace Jobsite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BedrijfsController : ControllerBase
    {
        private readonly JobsiteContext _context;

        public BedrijfsController(JobsiteContext context)
        {
            _context = context;
        }

        // GET: api/Bedrijfs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bedrijf>>> GetBedrijven(int? userId)
        {
            if (userId == null)
            {
                return await _context.Bedrijven.Include(x => x.Vacatures).ThenInclude(x => x.Sollicitaties).ToListAsync();
            }
            else
            {
                return await _context.Bedrijven.Where(x => x.UserId == userId).Include(x => x.Vacatures).ThenInclude(x => x.Sollicitaties).ToListAsync();
            }
        }

        // GET: api/Bedrijfs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bedrijf>> GetBedrijf(int id)
        {
            var bedrijf = await _context.Bedrijven.Include(x => x.Vacatures).FirstOrDefaultAsync(i => i.Id == id);

            if (bedrijf == null)
            {
                return NotFound();
            }

            return bedrijf;
        }

        // PUT: api/Bedrijfs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBedrijf(int id, Bedrijf bedrijf)
        {
            if (id != bedrijf.Id)
            {
                return BadRequest();
            }

            _context.Entry(bedrijf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BedrijfExists(id))
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

        // POST: api/Bedrijfs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Bedrijf>> PostBedrijf(Bedrijf bedrijf)
        {
            _context.Bedrijven.Add(bedrijf);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBedrijf", new { id = bedrijf.Id }, bedrijf);
        }

        // DELETE: api/Bedrijfs/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBedrijf(int id)
        {
            var bedrijf = await _context.Bedrijven.FindAsync(id);
            if (bedrijf == null)
            {
                return NotFound();
            }

            _context.Bedrijven.Remove(bedrijf);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BedrijfExists(int id)
        {
            return _context.Bedrijven.Any(e => e.Id == id);
        }
    }
}
