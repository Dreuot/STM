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
    public class TaskLabelsController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public TaskLabelsController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/TaskLabels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CTaskLabel>>> GetCTaskLabel()
        {
            return await _context.CTaskLabel.ToListAsync();
        }

        // GET: api/TaskLabels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CTaskLabel>> GetCTaskLabel(int id)
        {
            var cTaskLabel = await _context.CTaskLabel.FindAsync(id);

            if (cTaskLabel == null)
            {
                return NotFound();
            }

            return cTaskLabel;
        }

        // PUT: api/TaskLabels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCTaskLabel(int id, CTaskLabel cTaskLabel)
        {
            if (id != cTaskLabel.Id)
            {
                return BadRequest();
            }

            _context.Entry(cTaskLabel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CTaskLabelExists(id))
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

        // POST: api/TaskLabels
        [HttpPost]
        public async Task<ActionResult<CTaskLabel>> PostCTaskLabel(CTaskLabel cTaskLabel)
        {
            _context.CTaskLabel.Add(cTaskLabel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCTaskLabel", new { id = cTaskLabel.Id }, cTaskLabel);
        }

        // DELETE: api/TaskLabels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CTaskLabel>> DeleteCTaskLabel(int id)
        {
            var cTaskLabel = await _context.CTaskLabel.FindAsync(id);
            if (cTaskLabel == null)
            {
                return NotFound();
            }

            _context.CTaskLabel.Remove(cTaskLabel);
            await _context.SaveChangesAsync();

            return cTaskLabel;
        }

        private bool CTaskLabelExists(int id)
        {
            return _context.CTaskLabel.Any(e => e.Id == id);
        }
    }
}
