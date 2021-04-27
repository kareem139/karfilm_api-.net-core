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
   
   
   
    public class ShowsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Shows
        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<Shows>>> GetShows()
        {
            return await _context.Shows.ToListAsync();
        }

        // GET: api/Shows/5
        [HttpGet("{tvshowname}")]
        public async Task<ActionResult<Shows>> GetShows(string tvshowname)
        {
            var shows =await _context.Shows.FindAsync(tvshowname);
            //var shows = from b in _context.Shows
            //             where b.Tvshowname.Contains(tvshowname)
            //             select b;

            if (shows == null)
            {
                return NotFound();
            }

            return shows;
        }

        // PUT: api/Shows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShows(int id, Shows shows)
        {
            if (id != shows.Id)
            {
                return BadRequest();
            }

            _context.Entry(shows).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowsExists(id))
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

        // POST: api/Shows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shows>> PostShows(Shows shows)
        {
            _context.Shows.Add(shows);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShows", new { id = shows.Id }, shows);
        }

        // DELETE: api/Shows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShows(int id)
        {
            var shows = await _context.Shows.FindAsync(id);
            if (shows == null)
            {
                return NotFound();
            }

            _context.Shows.Remove(shows);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShowsExists(int id)
        {
            return _context.Shows.Any(e => e.Id == id);
        }

  
    }
}
