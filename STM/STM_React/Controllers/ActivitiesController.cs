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
    public class ActivitiesController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public ActivitiesController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CActivity>>> GetCActivity()
        {
            return await _context.CActivity.ToListAsync();
        }

        // GET: api/Activities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CActivity>> GetCActivity(int id)
        {
            var cActivity = await _context.CActivity.FindAsync(id);

            if (cActivity == null)
            {
                return NotFound();
            }

            return cActivity;
        }

        // PUT: api/Activities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCActivity(int id, CActivity cActivity)
        {
            if (id != cActivity.Id)
            {
                return BadRequest();
            }

            _context.Entry(cActivity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CActivityExists(id))
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

        // POST: api/Activities
        [HttpPost]
        public async Task<ActionResult<CActivity>> PostCActivity(CActivity cActivity)
        {
            _context.CActivity.Add(cActivity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCActivity", new { id = cActivity.Id }, cActivity);
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CActivity>> DeleteCActivity(int id)
        {
            var cActivity = await _context.CActivity.FindAsync(id);
            if (cActivity == null)
            {
                return NotFound();
            }

            _context.CActivity.Remove(cActivity);
            await _context.SaveChangesAsync();

            return cActivity;
        }

        private bool CActivityExists(int id)
        {
            return _context.CActivity.Any(e => e.Id == id);
        }
    }
}
