using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Jobsite.DAL.Data;
using Jobsite.DAL.Models;

namespace Jobsite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacaturesController : ControllerBase
    {
        private readonly JobsiteContext _context;

        public VacaturesController(JobsiteContext context)
        {
            _context = context;
        }

        // GET: api/Vacatures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vacature>>> GetVacatures(int? bedrijfId, string? titel, bool active = false)
        {
            if (bedrijfId == null && titel == null && !active)
            {
                return await _context.Vacatures.Include(s => s.Sollicitaties).Include(x => x.Bedrijf).ToListAsync();
            } 
            else if (bedrijfId == null && titel == null && active)
            {
                return await _context.Vacatures
                    .Where(x => x.DatumSluiting > DateTime.Now)
                    .Include(s => s.Sollicitaties)
                    .Include(x => x.Bedrijf).ToListAsync();
            }
            else if (bedrijfId != null && titel != null)
            {
                return await _context.Vacatures
                    .Where(x => x.Titel.Contains(titel))
                    .Where(x => x.BedrijfId == bedrijfId)
                    .Where(x => x.DatumSluiting > DateTime.Now)
                    .Include(s => s.Sollicitaties)
                    .Include(x => x.Bedrijf)
                    .ToListAsync();
            } else if (bedrijfId != null)
            {
                return await _context.Vacatures
                    .Where(x => x.BedrijfId == bedrijfId)
                    .Where(x => x.DatumSluiting > DateTime.Now)
                    .Include(s => s.Sollicitaties)
                    .Include(x => x.Bedrijf)
                    .ToListAsync();
            } else
            {
                return await _context.Vacatures
                    .Where(x => x.Titel.Contains(titel))
                    .Where(x => x.DatumSluiting > DateTime.Now)
                    .Include(s => s.Sollicitaties)
                    .Include(x => x.Bedrijf)
                    .ToListAsync();
            }
        }

        // GET: api/Vacatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vacature>> GetVacature(int id)
        {
            var vacature = await _context.Vacatures.Include(x => x.Sollicitaties).Include(x => x.Bedrijf).FirstOrDefaultAsync(i => i.Id == id);

            if (vacature == null)
            {
                return NotFound();
            }

            return vacature;
        }

        // PUT: api/Vacatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacature(int id, Vacature vacature)
        {
            if (id != vacature.Id)
            {
                return BadRequest();
            }

            _context.Entry(vacature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacatureExists(id))
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

        // POST: api/Vacatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vacature>> PostVacature(Vacature vacature)
        {
            _context.Vacatures.Add(vacature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVacature", new { id = vacature.Id }, vacature);
        }

        // DELETE: api/Vacatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacature(int id)
        {
            var vacature = await _context.Vacatures.FindAsync(id);
            if (vacature == null)
            {
                return NotFound();
            }

            _context.Vacatures.Remove(vacature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VacatureExists(int id)
        {
            return _context.Vacatures.Any(e => e.Id == id);
        }
    }
}
