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
    public class UsersController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public UsersController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CUser>>> GetCUser()
        {
            return await _context.CUser.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CUser>> GetCUser(int id)
        {
            var cUser = await _context.CUser.FindAsync(id);

            if (cUser == null)
            {
                return NotFound();
            }

            return cUser;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCUser(int id, CUser cUser)
        {
            if (id != cUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(cUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CUserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<CUser>> PostCUser(CUser cUser)
        {
            _context.CUser.Add(cUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCUser", new { id = cUser.Id }, cUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CUser>> DeleteCUser(int id)
        {
            var cUser = await _context.CUser.FindAsync(id);
            if (cUser == null)
            {
                return NotFound();
            }

            _context.CUser.Remove(cUser);
            await _context.SaveChangesAsync();

            return cUser;
        }

        private bool CUserExists(int id)
        {
            return _context.CUser.Any(e => e.Id == id);
        }
    }
}
