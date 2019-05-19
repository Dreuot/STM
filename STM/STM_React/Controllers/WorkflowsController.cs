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
    public class WorkflowsController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public WorkflowsController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/Workflows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CWorkflow>>> GetCWorkflow()
        {
            return await _context.CWorkflow.ToListAsync();
        }

        // GET: api/Workflows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CWorkflow>> GetCWorkflow(int id)
        {
            var cWorkflow = await _context.CWorkflow.FindAsync(id);

            if (cWorkflow == null)
            {
                return NotFound();
            }

            return cWorkflow;
        }

        // PUT: api/Workflows/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCWorkflow(int id, CWorkflow cWorkflow)
        {
            if (id != cWorkflow.Id)
            {
                return BadRequest();
            }

            _context.Entry(cWorkflow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CWorkflowExists(id))
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

        // POST: api/Workflows
        [HttpPost]
        public async Task<ActionResult<CWorkflow>> PostCWorkflow(CWorkflow cWorkflow)
        {
            _context.CWorkflow.Add(cWorkflow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCWorkflow", new { id = cWorkflow.Id }, cWorkflow);
        }

        // DELETE: api/Workflows/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CWorkflow>> DeleteCWorkflow(int id)
        {
            var cWorkflow = await _context.CWorkflow.FindAsync(id);
            if (cWorkflow == null)
            {
                return NotFound();
            }

            _context.CWorkflow.Remove(cWorkflow);
            await _context.SaveChangesAsync();

            return cWorkflow;
        }

        private bool CWorkflowExists(int id)
        {
            return _context.CWorkflow.Any(e => e.Id == id);
        }
    }
}
