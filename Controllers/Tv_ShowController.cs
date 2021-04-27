using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OlaApi.Models;

namespace OlaApi.Controllers
{
   
    [EnableCors("cor")]
    [Route("api/[controller]")]
    [ApiController]
    
    
    
    public class Tv_ShowController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public Tv_ShowController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tv_Show
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tv_Show>>> GetTv_Shows()
        {
            return await _context.Tv_Shows.ToListAsync();
        }

        // GET: api/Tv_Show/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tv_Show>> GetTv_Show(int id)
        {
            var tv_Show = await _context.Tv_Shows.FindAsync(id);

            if (tv_Show == null)
            {
                return NotFound();
            }

            return tv_Show;
        }

        // PUT: api/Tv_Show/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTv_Show(int id, Tv_Show tv_Show)
        {
            if (id != tv_Show.Id)
            {
                return BadRequest();
            }

            _context.Entry(tv_Show).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tv_ShowExists(id))
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

        // POST: api/Tv_Show
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tv_Show>> PostTv_Show(Tv_Show tv_Show)
        {
            _context.Tv_Shows.Add(tv_Show);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTv_Show", new { id = tv_Show.Id }, tv_Show);
        }

        // DELETE: api/Tv_Show/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTv_Show(int id)
        {
            var tv_Show = await _context.Tv_Shows.FindAsync(id);
            if (tv_Show == null)
            {
                return NotFound();
            }

            _context.Tv_Shows.Remove(tv_Show);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tv_ShowExists(int id)
        {
            return _context.Tv_Shows.Any(e => e.Id == id);
        }
    }
}
