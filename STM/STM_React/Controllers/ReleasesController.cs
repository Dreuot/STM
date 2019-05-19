using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STM_React.Models.Data;

namespace STM_React.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReleasesController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public ReleasesController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/Releases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CRelease>>> GetCRelease()
        {
            return await _context.CRelease.ToListAsync();
        }

        // GET: api/Releases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CRelease>> GetCRelease(int id)
        {
            var cRelease = await _context.CRelease.FindAsync(id);

            if (cRelease == null)
            {
                return NotFound();
            }

            return cRelease;
        }

        // PUT: api/Releases/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCRelease(int id, CRelease cRelease)
        {
            if (id != cRelease.Id)
            {
                return BadRequest();
            }

            _context.Entry(cRelease).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CReleaseExists(id))
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

        // POST: api/Releases
        [HttpPost]
        public async Task<ActionResult<CRelease>> PostCRelease(CRelease cRelease)
        {
            _context.CRelease.Add(cRelease);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCRelease", new { id = cRelease.Id }, cRelease);
        }

        // DELETE: api/Releases/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CRelease>> DeleteCRelease(int id)
        {
            var cRelease = await _context.CRelease.FindAsync(id);
            if (cRelease == null)
            {
                return NotFound();
            }

            _context.CRelease.Remove(cRelease);
            await _context.SaveChangesAsync();

            return cRelease;
        }

        private bool CReleaseExists(int id)
        {
            return _context.CRelease.Any(e => e.Id == id);
        }
    }
}
