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
    public class AttachesController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public AttachesController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/Attaches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CAttach>>> GetCAttach()
        {
            return await _context.CAttach.ToListAsync();
        }

        // GET: api/Attaches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CAttach>> GetCAttach(int id)
        {
            var cAttach = await _context.CAttach.FindAsync(id);

            if (cAttach == null)
            {
                return NotFound();
            }

            return cAttach;
        }

        // PUT: api/Attaches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCAttach(int id, CAttach cAttach)
        {
            if (id != cAttach.Id)
            {
                return BadRequest();
            }

            _context.Entry(cAttach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CAttachExists(id))
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

        // POST: api/Attaches
        [HttpPost]
        public async Task<ActionResult<CAttach>> PostCAttach(CAttach cAttach)
        {
            _context.CAttach.Add(cAttach);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCAttach", new { id = cAttach.Id }, cAttach);
        }

        // DELETE: api/Attaches/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CAttach>> DeleteCAttach(int id)
        {
            var cAttach = await _context.CAttach.FindAsync(id);
            if (cAttach == null)
            {
                return NotFound();
            }

            _context.CAttach.Remove(cAttach);
            await _context.SaveChangesAsync();

            return cAttach;
        }

        private bool CAttachExists(int id)
        {
            return _context.CAttach.Any(e => e.Id == id);
        }
    }
}
