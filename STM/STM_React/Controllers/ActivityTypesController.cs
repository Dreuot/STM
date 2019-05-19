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
    public class ActivityTypesController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public ActivityTypesController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/ActivityTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CActivityType>>> GetCActivityType()
        {
            return await _context.CActivityType.ToListAsync();
        }

        // GET: api/ActivityTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CActivityType>> GetCActivityType(int id)
        {
            var cActivityType = await _context.CActivityType.FindAsync(id);

            if (cActivityType == null)
            {
                return NotFound();
            }

            return cActivityType;
        }

        // PUT: api/ActivityTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCActivityType(int id, CActivityType cActivityType)
        {
            if (id != cActivityType.Id)
            {
                return BadRequest();
            }

            _context.Entry(cActivityType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CActivityTypeExists(id))
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

        // POST: api/ActivityTypes
        [HttpPost]
        public async Task<ActionResult<CActivityType>> PostCActivityType(CActivityType cActivityType)
        {
            _context.CActivityType.Add(cActivityType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCActivityType", new { id = cActivityType.Id }, cActivityType);
        }

        // DELETE: api/ActivityTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CActivityType>> DeleteCActivityType(int id)
        {
            var cActivityType = await _context.CActivityType.FindAsync(id);
            if (cActivityType == null)
            {
                return NotFound();
            }

            _context.CActivityType.Remove(cActivityType);
            await _context.SaveChangesAsync();

            return cActivityType;
        }

        private bool CActivityTypeExists(int id)
        {
            return _context.CActivityType.Any(e => e.Id == id);
        }
    }
}
