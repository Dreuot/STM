using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STM_React.Infrastructure;
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
        [Route("SetTimes")]
        public void SetTimes()
        {
            var rels = _context.CTaskRel.Where(rel => rel.RelType == "Блокирует" || rel.RelType == "Подзадача").Include(r => r.TaskMaster).Include(r => r.TaskSlave).ToList();
            var start = _context.CTask.FirstOrDefault(t => !rels.Exists(r => r.TaskMasterId == t.Id));
            Dictionary<int, int> distances = GetMatrix();
            foreach(var item in _context.CTask)
            {
                item.PlannedStart = AddWorkDay(start.PlannedStart.Value, distances[item.Id]);
                item.PlannedComplete = AddWorkDay(item.PlannedStart.Value, item.StoryPoints != null ? item.StoryPoints.Value : 0);
            }

            _context.SaveChanges();
        }

        private void CalcStartEndDates(CTask task, DateTime? startDate, IEnumerable<CTaskRel> rels)
        {
            if(startDate == null)
            {
                startDate = task.PlannedStart.Value;
            }
            else
            {
                task.PlannedStart = startDate;
            }

            DateTime? endDate = AddWorkDay(startDate.Value, (task.StoryPoints == null) ? 0 : task.StoryPoints.Value);


            task.PlannedComplete = endDate;

            foreach (var item in rels.Where(r => r.TaskSlaveId == task.Id).Select(r => r.TaskMaster))
            {
                CalcStartEndDates(item, endDate, rels);
            }
        }


        [HttpGet]
        [Route("GetMatrix")]
        public Dictionary<int, int> GetMatrix()
        {
            var taskCount = _context.CTask.Count();
            var tasks = _context.CTask.ToList();
            var rels = _context.CTaskRel.Where(r => r.RelType == "Блокируется" || r.RelType == "Родительская задача").Include(r => r.TaskMaster).Include(r => r.TaskSlave).ToList();
            var start = _context.CTask.FirstOrDefault(t => !rels.Exists(r => r.TaskSlaveId == t.Id));
            List<GraphLevel> levels = new List<GraphLevel>(taskCount);
            for(int i = 0; i < taskCount; i++)
            {
                levels.Add(new GraphLevel() { Level = i });
            }

            levels[0].Tasks.Add(start);
            for(var i = 0; i < taskCount - 1; i++)
            {
                for(var j = 0; j < levels[i].Tasks.Count(); j++)
                {
                    CalcChildLevels(levels[i].Tasks[j], rels, levels, i+1);
                }
            }

            List<CTask> directed = new List<CTask>(taskCount);
            foreach(var level in levels.Where(l => l.Tasks.Count > 0))
            {
                directed.AddRange(level.Tasks.Distinct());
            }

            int?[,] matrix = new int?[taskCount, taskCount];
            for(int i = 0; i < taskCount; i++)
            {
                for(int j = 0; j < taskCount; j++)
                {
                    var rel = rels.FirstOrDefault(r => r.TaskMasterId == directed[i].Id && r.TaskSlaveId == directed[j].Id);
                    matrix[i, j] = rel != null ? rel.TaskMaster.StoryPoints : null;
                }
            }

            Graph g = new Graph(matrix);
            g.Dynamic();
            int[] distancs = g.Distances();
            Dictionary<int, int> dir = new Dictionary<int, int>();
            for (var i = 0; i < directed.Count; i++)
            {
                dir.Add(directed[i].Id, distancs[i]);
            }

            return dir;
        }

        [HttpGet]
        [Route("MaxPath")]
        public int[] MaxPath()
        {
            var taskCount = _context.CTask.Count();
            var tasks = _context.CTask.ToList();
            var rels = _context.CTaskRel.Where(r => r.RelType == "Блокируется" || r.RelType == "Родительская задача").Include(r => r.TaskMaster).Include(r => r.TaskSlave).ToList();
            var start = _context.CTask.FirstOrDefault(t => !rels.Exists(r => r.TaskSlaveId == t.Id));
            var end = _context.CTask.FirstOrDefault(t => !rels.Exists(r => r.TaskMasterId == t.Id));
            List<GraphLevel> levels = new List<GraphLevel>(taskCount);
            for (int i = 0; i < taskCount; i++)
            {
                levels.Add(new GraphLevel() { Level = i });
            }

            levels[0].Tasks.Add(start);
            for (var i = 0; i < taskCount - 1; i++)
            {
                for (var j = 0; j < levels[i].Tasks.Count(); j++)
                {
                    CalcChildLevels(levels[i].Tasks[j], rels, levels, i + 1);
                }
            }

            List<CTask> directed = new List<CTask>(taskCount);
            foreach (var level in levels.Where(l => l.Tasks.Count > 0))
            {
                directed.AddRange(level.Tasks.Distinct());
            }

            int?[,] matrix = new int?[taskCount, taskCount];
            for (int i = 0; i < taskCount; i++)
            {
                for (int j = 0; j < taskCount; j++)
                {
                    var rel = rels.FirstOrDefault(r => r.TaskMasterId == directed[i].Id && r.TaskSlaveId == directed[j].Id);
                    matrix[i, j] = rel != null ? rel.TaskMaster.StoryPoints : null;
                }
            }

            Graph g = new Graph(matrix);
            g.Dynamic();
            int[] distancs = g.Distances();
            Dictionary<int, int> dir = new Dictionary<int, int>();
            for (var i = 0; i < directed.Count; i++)
            {
                dir.Add(directed[i].Id, distancs[i]);
            }

            Func<IEnumerable<CTask>, int> getMax = (IEnumerable<CTask> t) =>
              {
                  var related = dir.Where(kv => t.Any(ts => ts.Id == kv.Key));
                  var max = related.Select(r => r.Value).Max();
                  return related.FirstOrDefault(kv => kv.Value == max).Key;
              };



            List<int> result = new List<int>();
            result.Add(end.Id);
            CTask prev = end;
            do
            {
                var prevs = rels.Where(r => r.TaskSlaveId == prev.Id).Select(t => t.TaskMaster);
                prev = prevs.Where(p => p.Id == getMax(prevs)).FirstOrDefault();
                result.Add(prev.Id);
            }
            while (prev.Id != start.Id);
            result.Reverse();
            return result.ToArray();
        }

        private void CalcChildLevels(CTask root, IEnumerable<CTaskRel> rels, List<GraphLevel> levels, int level)
        {
            var childs = rels.Where(r => r.TaskMasterId == root.Id).Select(r => r.TaskSlave).ToList();
            List<CTask> parents = new List<CTask>();
            for(int i = 0; i < level; i++)
            {
                parents.AddRange(levels[i].Tasks);
            }

            var notDirectChild = childs.Where(c => rels.Where(r => r.TaskSlaveId == c.Id).Any(r1 => !parents.Exists(p => p.Id == r1.TaskMasterId)));
            levels[level].Tasks.AddRange(childs.Where(c => !notDirectChild.Any(nc => nc.Id == c.Id)));
        }

        private DateTime? AddWorkDay(DateTime date, int day)
        {
            int dir = day > 0 ? 1 : -1;
            while(day != 0)
            {
                date = date.AddDays(dir);

                if (!(date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday))
                {
                    day -= dir;
                }

            }

            return date;
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
        [Route("Directed")]
        public async Task<ActionResult<IEnumerable<CTaskRel>>> GetTaskDirRel()
        {
            return await _context.CTaskRel.Where(rel => rel.RelType == "Блокирует" || rel.RelType == "Подзадача").Include(rel => rel.TaskMaster).Include(rel => rel.TaskSlave).ToListAsync();
        }

        // GET: api/TaskRels
        [HttpGet]
        [Route("BackwardDirected")]
        public async Task<ActionResult<IEnumerable<CTaskRel>>> GetTaskBackDirRel()
        {
            return await _context.CTaskRel.Where(rel => rel.RelType == "Блокируется" || rel.RelType == "Родительская задача").Include(rel => rel.TaskMaster).Include(rel => rel.TaskSlave).ToListAsync();
        }

        // GET: api/TaskRels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CTaskRel>>> GetCTaskRel()
        {
            return await _context.CTaskRel.Include(rel => rel.TaskMaster).Include(rel => rel.TaskSlave).ToListAsync();
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
