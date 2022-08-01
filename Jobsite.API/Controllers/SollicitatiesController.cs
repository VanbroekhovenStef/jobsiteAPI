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
    public class SollicitatiesController : ControllerBase
    {
        private readonly JobsiteContext _context;

        public SollicitatiesController(JobsiteContext context)
        {
            _context = context;
        }

        // GET: api/Sollicitaties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sollicitatie>>> GetSollicitaties(int? userId, int? vacatureId)
        {
            if (userId != null && vacatureId != null)
            {
                return await _context.Sollicitaties
                    .Where(x => x.VacatureId == vacatureId)
                    .Where(x => x.UserId == userId)
                    .Include(x => x.User)
                    .Include(x => x.Vacature)
                    .ToListAsync();
            }
            if (vacatureId != null)
            {
                return await _context.Sollicitaties.Where(x => x.VacatureId == vacatureId).Include(x => x.User).Include(x => x.Vacature).ToListAsync();
            } else if(userId != null)
            {
                return await _context.Sollicitaties.Where(x => x.UserId == userId).Include(x => x.Vacature).ThenInclude(x => x.Bedrijf).Include(x => x.Vacature).ThenInclude(x => x.Sollicitaties).ToListAsync();
            } else
            {
                return NotFound();
            }
        }

        /*        [HttpGet]
                public async Task<ActionResult<IEnumerable<Sollicitatie>>> GetSollicitatiesFromUser()
                {
                    return await _context.Sollicitaties.Include(x => x.User).Include(x => x.Vacature).ToListAsync();
                }*/

        // GET: api/Sollicitaties/5
        [HttpGet("FromUserOnVacature")]
        public async Task<ActionResult<Sollicitatie>> GetSollicitatieFromUserOnVacature(int? userId, int? vacatureId)
        {
            if (userId != null && vacatureId != null)
            {
                var sollicitatie =  await _context.Sollicitaties
                    .Where(x => x.VacatureId == vacatureId)
                    .Where(x => x.UserId == userId)
/*                    .Include(x => x.User)
                    .Include(x => x.Vacature)*/
                    .FirstOrDefaultAsync();

                if (sollicitatie == null)
                {
                    return NotFound();
                } else
                {
                    return sollicitatie;
                }
            } else
            {
                return NotFound();
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Sollicitatie>> GetSollicitatie(int id)
        {
            var sollicitatie = await _context.Sollicitaties.FindAsync(id);

            if (sollicitatie == null)
            {
                return NotFound();
            }

            return sollicitatie;
        }

        // PUT: api/Sollicitaties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSollicitatie(int id, Sollicitatie sollicitatie)
        {
            if (id != sollicitatie.Id)
            {
                return BadRequest();
            }

            _context.Entry(sollicitatie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SollicitatieExists(id))
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

        // POST: api/Sollicitaties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sollicitatie>> PostSollicitatie(Sollicitatie sollicitatie)
        {
            _context.Sollicitaties.Add(sollicitatie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSollicitatie", new { id = sollicitatie.Id }, sollicitatie);
        }

        // DELETE: api/Sollicitaties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSollicitatie(int id)
        {
            var sollicitatie = await _context.Sollicitaties.FindAsync(id);
            if (sollicitatie == null)
            {
                return NotFound();
            }

            _context.Sollicitaties.Remove(sollicitatie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SollicitatieExists(int id)
        {
            return _context.Sollicitaties.Any(e => e.Id == id);
        }
    }
}
