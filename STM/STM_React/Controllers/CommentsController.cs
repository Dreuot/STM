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
    public class CommentsController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public CommentsController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CComment>>> GetCComment()
        {
            return await _context.CComment.ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CComment>> GetCComment(int id)
        {
            var cComment = await _context.CComment.FindAsync(id);

            if (cComment == null)
            {
                return NotFound();
            }

            return cComment;
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCComment(int id, CComment cComment)
        {
            if (id != cComment.Id)
            {
                return BadRequest();
            }

            _context.Entry(cComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CCommentExists(id))
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

        // POST: api/Comments
        [HttpPost]
        public async Task<ActionResult<CComment>> PostCComment(CComment cComment)
        {
            _context.CComment.Add(cComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCComment", new { id = cComment.Id }, cComment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CComment>> DeleteCComment(int id)
        {
            var cComment = await _context.CComment.FindAsync(id);
            if (cComment == null)
            {
                return NotFound();
            }

            _context.CComment.Remove(cComment);
            await _context.SaveChangesAsync();

            return cComment;
        }

        private bool CCommentExists(int id)
        {
            return _context.CComment.Any(e => e.Id == id);
        }
    }
}
