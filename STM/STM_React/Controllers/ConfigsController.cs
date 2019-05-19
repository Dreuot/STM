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
    public class ConfigsController : ControllerBase
    {
        private readonly STM_DBContext _context;

        public ConfigsController(STM_DBContext context)
        {
            _context = context;
        }

        // GET: api/Configs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CConfig>>> GetCConfig()
        {
            return await _context.CConfig.ToListAsync();
        }

        // GET: api/Configs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CConfig>> GetCConfig(int id)
        {
            var cConfig = await _context.CConfig.FindAsync(id);

            if (cConfig == null)
            {
                return NotFound();
            }

            return cConfig;
        }

        // PUT: api/Configs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCConfig(int id, CConfig cConfig)
        {
            if (id != cConfig.Id)
            {
                return BadRequest();
            }

            _context.Entry(cConfig).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CConfigExists(id))
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

        // POST: api/Configs
        [HttpPost]
        public async Task<ActionResult<CConfig>> PostCConfig(CConfig cConfig)
        {
            _context.CConfig.Add(cConfig);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCConfig", new { id = cConfig.Id }, cConfig);
        }

        // DELETE: api/Configs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CConfig>> DeleteCConfig(int id)
        {
            var cConfig = await _context.CConfig.FindAsync(id);
            if (cConfig == null)
            {
                return NotFound();
            }

            _context.CConfig.Remove(cConfig);
            await _context.SaveChangesAsync();

            return cConfig;
        }

        private bool CConfigExists(int id)
        {
            return _context.CConfig.Any(e => e.Id == id);
        }
    }
}
