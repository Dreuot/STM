using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STM_React.Models.Data;

namespace STM_React.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public ProjectsController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CProject>>> GetCProject()
        {
            var x = await _context.CProject.Include(p => p.ManagerNavigation).ToListAsync();
            return x;
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CProject>> GetCProject(int id)
        {
            var cProject = await _context.CProject.Include(p => p.ManagerNavigation).Where(p => p.Id == id).FirstOrDefaultAsync();

            if (cProject == null)
            {
                return NotFound();
            }

            return cProject;
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCProject(int id, CProject cProject)
        {
            if (id != cProject.Id)
            {
                return BadRequest();
            }

            _context.Entry(cProject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CProjectExists(id))
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

        // POST: api/Projects
        [HttpPost]
        public ActionResult<CProject> PostCProject(CProject cProject)
        {
            _context.CProject.Add(cProject);
            _context.SaveChanges();

            return CreatedAtAction("GetCProject", new { id = cProject.Id }, cProject);
        }

        //// POST: api/Projects
        //[HttpPost]
        //public async Task<ActionResult<CProject>> PostCProject(CProject cProject)
        //{
        //    _context.CProject.Add(cProject);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCProject", new { id = cProject.Id }, cProject);
        //}

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CProject>> DeleteCProject(int id)
        {
            var cProject = await _context.CProject.FindAsync(id);
            if (cProject == null)
            {
                return NotFound();
            }

            _context.CProject.Remove(cProject);
            await _context.SaveChangesAsync();

            return cProject;
        }

        private bool CProjectExists(int id)
        {
            return _context.CProject.Any(e => e.Id == id);
        }
    }
}
