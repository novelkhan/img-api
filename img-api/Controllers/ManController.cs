using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using img_api.Data;
using img_api.Models;

namespace img_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ManController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Man
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Man>>> GetMen()
        {
          if (_context.Men == null)
          {
              return NotFound();
          }
            return await _context.Men.ToListAsync();
        }

        // GET: api/Man/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Man>> GetMan(int id)
        {
          if (_context.Men == null)
          {
              return NotFound();
          }
            var man = await _context.Men.FindAsync(id);

            if (man == null)
            {
                return NotFound();
            }

            return man;
        }

        // PUT: api/Man/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMan(int id, Man man)
        {
            if (id != man.Id)
            {
                return BadRequest();
            }

            _context.Entry(man).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManExists(id))
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

        // POST: api/Man
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Man>> PostMan(Man man)
        {
          if (_context.Men == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Men'  is null.");
          }
            _context.Men.Add(man);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetMan", new { id = man.Id }, man);
            return Ok(new JsonResult(new { title = "Man Added", message = "The man has been added" }));
        }

        // DELETE: api/Man/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMan(int id)
        {
            if (_context.Men == null)
            {
                return NotFound();
            }
            var man = await _context.Men.FindAsync(id);
            if (man == null)
            {
                return NotFound();
            }

            _context.Men.Remove(man);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ManExists(int id)
        {
            return (_context.Men?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
