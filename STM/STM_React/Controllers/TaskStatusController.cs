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
    public class TaskStatusController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public TaskStatusController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/TaskStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CTaskStatus>>> GetCTaskStatus()
        {
            return await _context.CTaskStatus.ToListAsync();
        }

        // GET: api/TaskStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CTaskStatus>> GetCTaskStatus(int id)
        {
            var cTaskStatus = await _context.CTaskStatus.FindAsync(id);

            if (cTaskStatus == null)
            {
                return NotFound();
            }

            return cTaskStatus;
        }

        // PUT: api/TaskStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCTaskStatus(int id, CTaskStatus cTaskStatus)
        {
            if (id != cTaskStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(cTaskStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CTaskStatusExists(id))
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

        // POST: api/TaskStatus
        [HttpPost]
        public async Task<ActionResult<CTaskStatus>> PostCTaskStatus(CTaskStatus cTaskStatus)
        {
            _context.CTaskStatus.Add(cTaskStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCTaskStatus", new { id = cTaskStatus.Id }, cTaskStatus);
        }

        // DELETE: api/TaskStatus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CTaskStatus>> DeleteCTaskStatus(int id)
        {
            var cTaskStatus = await _context.CTaskStatus.FindAsync(id);
            if (cTaskStatus == null)
            {
                return NotFound();
            }

            _context.CTaskStatus.Remove(cTaskStatus);
            await _context.SaveChangesAsync();

            return cTaskStatus;
        }

        private bool CTaskStatusExists(int id)
        {
            return _context.CTaskStatus.Any(e => e.Id == id);
        }
    }
}
