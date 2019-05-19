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
    public class TaskTypesController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public TaskTypesController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/TaskTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CTaskType>>> GetCTaskType()
        {
            return await _context.CTaskType.ToListAsync();
        }

        // GET: api/TaskTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CTaskType>> GetCTaskType(int id)
        {
            var cTaskType = await _context.CTaskType.FindAsync(id);

            if (cTaskType == null)
            {
                return NotFound();
            }

            return cTaskType;
        }

        // PUT: api/TaskTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCTaskType(int id, CTaskType cTaskType)
        {
            if (id != cTaskType.Id)
            {
                return BadRequest();
            }

            _context.Entry(cTaskType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CTaskTypeExists(id))
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

        // POST: api/TaskTypes
        [HttpPost]
        public async Task<ActionResult<CTaskType>> PostCTaskType(CTaskType cTaskType)
        {
            _context.CTaskType.Add(cTaskType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCTaskType", new { id = cTaskType.Id }, cTaskType);
        }

        // DELETE: api/TaskTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CTaskType>> DeleteCTaskType(int id)
        {
            var cTaskType = await _context.CTaskType.FindAsync(id);
            if (cTaskType == null)
            {
                return NotFound();
            }

            _context.CTaskType.Remove(cTaskType);
            await _context.SaveChangesAsync();

            return cTaskType;
        }

        private bool CTaskTypeExists(int id)
        {
            return _context.CTaskType.Any(e => e.Id == id);
        }
    }
}
