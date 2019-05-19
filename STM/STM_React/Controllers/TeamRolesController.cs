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
    public class TeamRolesController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public TeamRolesController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/TeamRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CTeamRole>>> GetCTeamRole()
        {
            return await _context.CTeamRole.ToListAsync();
        }

        // GET: api/TeamRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CTeamRole>> GetCTeamRole(int id)
        {
            var cTeamRole = await _context.CTeamRole.FindAsync(id);

            if (cTeamRole == null)
            {
                return NotFound();
            }

            return cTeamRole;
        }

        // PUT: api/TeamRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCTeamRole(int id, CTeamRole cTeamRole)
        {
            if (id != cTeamRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(cTeamRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CTeamRoleExists(id))
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

        // POST: api/TeamRoles
        [HttpPost]
        public async Task<ActionResult<CTeamRole>> PostCTeamRole(CTeamRole cTeamRole)
        {
            _context.CTeamRole.Add(cTeamRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCTeamRole", new { id = cTeamRole.Id }, cTeamRole);
        }

        // DELETE: api/TeamRoles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CTeamRole>> DeleteCTeamRole(int id)
        {
            var cTeamRole = await _context.CTeamRole.FindAsync(id);
            if (cTeamRole == null)
            {
                return NotFound();
            }

            _context.CTeamRole.Remove(cTeamRole);
            await _context.SaveChangesAsync();

            return cTeamRole;
        }

        private bool CTeamRoleExists(int id)
        {
            return _context.CTeamRole.Any(e => e.Id == id);
        }
    }
}
