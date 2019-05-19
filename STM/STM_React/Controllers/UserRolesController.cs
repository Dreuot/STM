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
    public class UserRolesController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public UserRolesController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/UserRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CUserRole>>> GetCUserRole()
        {
            return await _context.CUserRole.ToListAsync();
        }

        // GET: api/UserRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CUserRole>> GetCUserRole(int id)
        {
            var cUserRole = await _context.CUserRole.FindAsync(id);

            if (cUserRole == null)
            {
                return NotFound();
            }

            return cUserRole;
        }

        // PUT: api/UserRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCUserRole(int id, CUserRole cUserRole)
        {
            if (id != cUserRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(cUserRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CUserRoleExists(id))
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

        // POST: api/UserRoles
        [HttpPost]
        public async Task<ActionResult<CUserRole>> PostCUserRole(CUserRole cUserRole)
        {
            _context.CUserRole.Add(cUserRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCUserRole", new { id = cUserRole.Id }, cUserRole);
        }

        // DELETE: api/UserRoles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CUserRole>> DeleteCUserRole(int id)
        {
            var cUserRole = await _context.CUserRole.FindAsync(id);
            if (cUserRole == null)
            {
                return NotFound();
            }

            _context.CUserRole.Remove(cUserRole);
            await _context.SaveChangesAsync();

            return cUserRole;
        }

        private bool CUserRoleExists(int id)
        {
            return _context.CUserRole.Any(e => e.Id == id);
        }
    }
}
