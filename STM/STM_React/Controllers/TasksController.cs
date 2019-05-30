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
    public class TasksController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public TasksController(STM_DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Board")]
        public async Task<ActionResult<IEnumerable<CTask>>> GetTasksForDesc()
        {
            return await _context.CTask.Include(t => t.Status).Include(t => t.Type).Include(t => t.Priority).Include(t => t.Assignee).OrderByDescending(t => t.Priority.PriorNum).ToListAsync();
        }

        [HttpGet]
        [Route("Code/{code}")]
        public async Task<ActionResult<CTask>> GetCTaskByCode(string code)
        {
            //Include(t => t.Project)
            var cTask = await _context.CTask
                .Include(t => t.Project)
                .Include(t => t.Status)
                //.Include(t => t.Project)
                .Include(t => t.CTaskRelTaskMaster).ThenInclude(tr => tr.TaskSlave)
                .Include(t => t.CreatedBy)
                .Include(t => t.Type)
                .Include(t => t.Priority)
                .FirstOrDefaultAsync(t => t.Code == code);

            if (cTask == null)
            {
                return NotFound();
            }

            return cTask;
        }

        [HttpGet]
        [Route("Project/{id}")]
        public async Task<ActionResult<IEnumerable<CTask>>> GetProjectTasks(int id)
        {
            return await _context.CTask.Where(t => t.ProjectId == id).Include(t => t.Assignee).Include(t => t.Status).Include(t => t.Type).Include(t => t.Priority).ToListAsync();
        }

        [HttpGet]
        [Route("Lite/Not/{id}")]
        public async Task<ActionResult<IEnumerable<CTask>>> GetCTaskLiteNot(int id)
        {
            return await _context.CTask.Where(t => t.Id != id).ToListAsync();
        }

        [HttpGet]
        [Route("LiteNotRels/{id}")]
        public async Task<ActionResult<IEnumerable<CTask>>> GetCTaskLiteNotRels(int id)
        {
            var rels = _context.CTaskRel.Where(rel => rel.TaskMasterId == id).Select(rel => rel.TaskSlaveId);
            return await _context.CTask.Where(t => t.Id != id && !rels.Any(slave => slave == t.Id)).ToListAsync();
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CTask>>> GetCTask()
        {
            return await _context.CTask.Include(t => t.Assignee).Include(t => t.Status).Include(t => t.Type).Include(t => t.Priority).ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CTask>> GetCTask(int id)
        {
            //Include(t => t.Project)
            var cTask = await _context.CTask
                .Include(t => t.Project)
                .Include(t => t.Status)
                .Include(t => t.Type)
                .Include(t => t.Priority)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (cTask == null)
            {
                return NotFound();
            }

            return cTask;
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCTask(int id, CTask cTask)
        {
            if (id != cTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(cTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CTaskExists(id))
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

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<CTask>> PostCTask(CTask cTask)
        {
            _context.CTask.Add(cTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCTask", new { id = cTask.Id }, cTask);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CTask>> DeleteCTask(int id)
        {
            var cTask = await _context.CTask.FindAsync(id);
            if (cTask == null)
            {
                return NotFound();
            }

            _context.CTask.Remove(cTask);
            await _context.SaveChangesAsync();

            return cTask;
        }

        private bool CTaskExists(int id)
        {
            return _context.CTask.Any(e => e.Id == id);
        }
    }
}
