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
    public class TaskPrioritiesController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public TaskPrioritiesController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/TaskPriorities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CTaskPriority>>> GetCTaskPriority()
        {
            return await _context.CTaskPriority.ToListAsync();
        }

        // GET: api/TaskPriorities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CTaskPriority>> GetCTaskPriority(int id)
        {
            var cTaskPriority = await _context.CTaskPriority.FindAsync(id);

            if (cTaskPriority == null)
            {
                return NotFound();
            }

            return cTaskPriority;
        }

        // PUT: api/TaskPriorities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCTaskPriority(int id, CTaskPriority cTaskPriority)
        {
            if (id != cTaskPriority.Id)
            {
                return BadRequest();
            }

            _context.Entry(cTaskPriority).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CTaskPriorityExists(id))
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

        // POST: api/TaskPriorities
        [HttpPost]
        public async Task<ActionResult<CTaskPriority>> PostCTaskPriority(CTaskPriority cTaskPriority)
        {
            _context.CTaskPriority.Add(cTaskPriority);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCTaskPriority", new { id = cTaskPriority.Id }, cTaskPriority);
        }

        // DELETE: api/TaskPriorities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CTaskPriority>> DeleteCTaskPriority(int id)
        {
            var cTaskPriority = await _context.CTaskPriority.FindAsync(id);
            if (cTaskPriority == null)
            {
                return NotFound();
            }

            _context.CTaskPriority.Remove(cTaskPriority);
            await _context.SaveChangesAsync();

            return cTaskPriority;
        }

        private bool CTaskPriorityExists(int id)
        {
            return _context.CTaskPriority.Any(e => e.Id == id);
        }
    }
}
