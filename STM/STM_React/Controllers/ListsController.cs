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
    public class ListsController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public ListsController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/Lists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CList>>> GetCList()
        {
            return await _context.CList.ToListAsync();
        }

        // GET: api/Lists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CList>> GetCList(int id)
        {
            var cList = await _context.CList.FindAsync(id);

            if (cList == null)
            {
                return NotFound();
            }

            return cList;
        }

        // PUT: api/Lists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCList(int id, CList cList)
        {
            if (id != cList.Id)
            {
                return BadRequest();
            }

            _context.Entry(cList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CListExists(id))
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

        // POST: api/Lists
        [HttpPost]
        public async Task<ActionResult<CList>> PostCList(CList cList)
        {
            _context.CList.Add(cList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCList", new { id = cList.Id }, cList);
        }

        // DELETE: api/Lists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CList>> DeleteCList(int id)
        {
            var cList = await _context.CList.FindAsync(id);
            if (cList == null)
            {
                return NotFound();
            }

            _context.CList.Remove(cList);
            await _context.SaveChangesAsync();

            return cList;
        }

        private bool CListExists(int id)
        {
            return _context.CList.Any(e => e.Id == id);
        }
    }
}
