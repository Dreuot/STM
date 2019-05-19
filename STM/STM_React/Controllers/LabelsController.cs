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
    public class LabelsController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public LabelsController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/Labels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CLabel>>> GetCLabel()
        {
            return await _context.CLabel.ToListAsync();
        }

        // GET: api/Labels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CLabel>> GetCLabel(int id)
        {
            var cLabel = await _context.CLabel.FindAsync(id);

            if (cLabel == null)
            {
                return NotFound();
            }

            return cLabel;
        }

        // PUT: api/Labels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCLabel(int id, CLabel cLabel)
        {
            if (id != cLabel.Id)
            {
                return BadRequest();
            }

            _context.Entry(cLabel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CLabelExists(id))
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

        // POST: api/Labels
        [HttpPost]
        public async Task<ActionResult<CLabel>> PostCLabel(CLabel cLabel)
        {
            _context.CLabel.Add(cLabel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCLabel", new { id = cLabel.Id }, cLabel);
        }

        // DELETE: api/Labels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CLabel>> DeleteCLabel(int id)
        {
            var cLabel = await _context.CLabel.FindAsync(id);
            if (cLabel == null)
            {
                return NotFound();
            }

            _context.CLabel.Remove(cLabel);
            await _context.SaveChangesAsync();

            return cLabel;
        }

        private bool CLabelExists(int id)
        {
            return _context.CLabel.Any(e => e.Id == id);
        }
    }
}
