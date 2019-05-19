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
    public class UserTeamsController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public UserTeamsController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/UserTeams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CUserTeam>>> GetCUserTeam()
        {
            return await _context.CUserTeam.ToListAsync();
        }

        // GET: api/UserTeams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CUserTeam>> GetCUserTeam(int id)
        {
            var cUserTeam = await _context.CUserTeam.FindAsync(id);

            if (cUserTeam == null)
            {
                return NotFound();
            }

            return cUserTeam;
        }

        // PUT: api/UserTeams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCUserTeam(int id, CUserTeam cUserTeam)
        {
            if (id != cUserTeam.Id)
            {
                return BadRequest();
            }

            _context.Entry(cUserTeam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CUserTeamExists(id))
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

        // POST: api/UserTeams
        [HttpPost]
        public async Task<ActionResult<CUserTeam>> PostCUserTeam(CUserTeam cUserTeam)
        {
            _context.CUserTeam.Add(cUserTeam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCUserTeam", new { id = cUserTeam.Id }, cUserTeam);
        }

        // DELETE: api/UserTeams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CUserTeam>> DeleteCUserTeam(int id)
        {
            var cUserTeam = await _context.CUserTeam.FindAsync(id);
            if (cUserTeam == null)
            {
                return NotFound();
            }

            _context.CUserTeam.Remove(cUserTeam);
            await _context.SaveChangesAsync();

            return cUserTeam;
        }

        private bool CUserTeamExists(int id)
        {
            return _context.CUserTeam.Any(e => e.Id == id);
        }
    }
}
