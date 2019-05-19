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
    public class RolesController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public RolesController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CRole>>> GetCRole()
        {
            return await _context.CRole.ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CRole>> GetCRole(int id)
        {
            var cRole = await _context.CRole.FindAsync(id);

            if (cRole == null)
            {
                return NotFound();
            }

            return cRole;
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCRole(int id, CRole cRole)
        {
            if (id != cRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(cRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CRoleExists(id))
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

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<CRole>> PostCRole(CRole cRole)
        {
            _context.CRole.Add(cRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCRole", new { id = cRole.Id }, cRole);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CRole>> DeleteCRole(int id)
        {
            var cRole = await _context.CRole.FindAsync(id);
            if (cRole == null)
            {
                return NotFound();
            }

            _context.CRole.Remove(cRole);
            await _context.SaveChangesAsync();

            return cRole;
        }

        private bool CRoleExists(int id)
        {
            return _context.CRole.Any(e => e.Id == id);
        }
    }
}
