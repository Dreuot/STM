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
    public class TasksController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public TasksController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CTask>>> GetCTask()
        {
            return await _context.CTask.Include(t => t.Assignee).Include(t => t.Status).Include(t => t.Type).Include(t => t.Priority).ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CTask>> GetCTask(int id)
        {
            var cTask = await _context.CTask.FindAsync(id);

            if (cTask == null)
            {
                return NotFound();
            }

            return cTask;
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCTask(int id, CTask cTask)
        {
            if (id != cTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(cTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CTaskExists(id))
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

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<CTask>> PostCTask(CTask cTask)
        {
            _context.CTask.Add(cTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCTask", new { id = cTask.Id }, cTask);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CTask>> DeleteCTask(int id)
        {
            var cTask = await _context.CTask.FindAsync(id);
            if (cTask == null)
            {
                return NotFound();
            }

            _context.CTask.Remove(cTask);
            await _context.SaveChangesAsync();

            return cTask;
        }

        private bool CTaskExists(int id)
        {
            return _context.CTask.Any(e => e.Id == id);
        }
    }
}
