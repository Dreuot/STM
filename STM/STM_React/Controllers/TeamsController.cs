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
    public class TeamsController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public TeamsController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CTeam>>> GetCTeam()
        {
            return await _context.CTeam.ToListAsync();
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CTeam>> GetCTeam(int id)
        {
            var cTeam = await _context.CTeam.FindAsync(id);

            if (cTeam == null)
            {
                return NotFound();
            }

            return cTeam;
        }

        // PUT: api/Teams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCTeam(int id, CTeam cTeam)
        {
            if (id != cTeam.Id)
            {
                return BadRequest();
            }

            _context.Entry(cTeam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CTeamExists(id))
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

        // POST: api/Teams
        [HttpPost]
        public async Task<ActionResult<CTeam>> PostCTeam(CTeam cTeam)
        {
            _context.CTeam.Add(cTeam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCTeam", new { id = cTeam.Id }, cTeam);
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CTeam>> DeleteCTeam(int id)
        {
            var cTeam = await _context.CTeam.FindAsync(id);
            if (cTeam == null)
            {
                return NotFound();
            }

            _context.CTeam.Remove(cTeam);
            await _context.SaveChangesAsync();

            return cTeam;
        }

        private bool CTeamExists(int id)
        {
            return _context.CTeam.Any(e => e.Id == id);
        }
    }
}
