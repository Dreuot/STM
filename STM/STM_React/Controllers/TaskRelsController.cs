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
    public class TaskRelsController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public TaskRelsController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/TaskRels
        [HttpGet]
        [Route("MasterId/{id}")]
        public async Task<ActionResult<IEnumerable<CTaskRel>>> GetRelatedByMaster(int id)
        {
            return await _context.CTaskRel.Where(rel => rel.TaskMasterId == id).Include(rel => rel.TaskSlave).ToListAsync();
        }

        // GET: api/TaskRels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CTaskRel>>> GetCTaskRel()
        {
            return await _context.CTaskRel.ToListAsync();
        }

        // GET: api/TaskRels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CTaskRel>> GetCTaskRel(int id)
        {
            var cTaskRel = await _context.CTaskRel.FindAsync(id);

            if (cTaskRel == null)
            {
                return NotFound();
            }

            return cTaskRel;
        }

        // PUT: api/TaskRels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCTaskRel(int id, CTaskRel cTaskRel)
        {
            if (id != cTaskRel.Id)
            {
                return BadRequest();
            }

            _context.Entry(cTaskRel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CTaskRelExists(id))
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

        // POST: api/TaskRels
        [HttpPost]
        public async Task<ActionResult<CTaskRel>> PostCTaskRel(CTaskRel cTaskRel)
        {
            _context.CTaskRel.Add(cTaskRel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCTaskRel", new { id = cTaskRel.Id }, cTaskRel);
        }

        // DELETE: api/TaskRels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CTaskRel>> DeleteCTaskRel(int id)
        {
            var cTaskRel = await _context.CTaskRel.FindAsync(id);
            if (cTaskRel == null)
            {
                return NotFound();
            }

            _context.CTaskRel.Remove(cTaskRel);
            await _context.SaveChangesAsync();

            return cTaskRel;
        }

        private bool CTaskRelExists(int id)
        {
            return _context.CTaskRel.Any(e => e.Id == id);
        }
    }
}
