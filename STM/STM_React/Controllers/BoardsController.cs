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
    public class BoardsController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public BoardsController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/Boards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CBoard>>> GetCBoard()
        {
            return await _context.CBoard.ToListAsync();
        }

        // GET: api/Boards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CBoard>> GetCBoard(int id)
        {
            var cBoard = await _context.CBoard.FindAsync(id);

            if (cBoard == null)
            {
                return NotFound();
            }

            return cBoard;
        }

        // PUT: api/Boards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCBoard(int id, CBoard cBoard)
        {
            if (id != cBoard.Id)
            {
                return BadRequest();
            }

            _context.Entry(cBoard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CBoardExists(id))
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

        // POST: api/Boards
        [HttpPost]
        public async Task<ActionResult<CBoard>> PostCBoard(CBoard cBoard)
        {
            _context.CBoard.Add(cBoard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCBoard", new { id = cBoard.Id }, cBoard);
        }

        // DELETE: api/Boards/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CBoard>> DeleteCBoard(int id)
        {
            var cBoard = await _context.CBoard.FindAsync(id);
            if (cBoard == null)
            {
                return NotFound();
            }

            _context.CBoard.Remove(cBoard);
            await _context.SaveChangesAsync();

            return cBoard;
        }

        private bool CBoardExists(int id)
        {
            return _context.CBoard.Any(e => e.Id == id);
        }
    }
}
