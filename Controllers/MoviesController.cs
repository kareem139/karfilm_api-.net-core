using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OlaApi.Models;
using OlaApi.Services;
using OlaApi.ViewModels;

namespace OlaApi.Controllers
{
    
    [EnableCors("cor")]
    [Route("api/[controller]")]
    [ApiController]
   
    
    
    

    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        [Obsolete]
        private readonly IHostingEnvironment _hosting;


        [Obsolete]
        public MoviesController(ApplicationDbContext context, IHostingEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;

        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            //var newmov = new Movie
            //{
            //    Name = movie.Name,
            //    Url = movie.Url,
            //    Categoryname = movie.Categoryname,
            //    IsActive = movie.IsActive,
            //    Img = $"{_hosting.WebRootPath}/images/{movie.Img}"
            //};
            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);





        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }



        [HttpPost]

        [DisableRequestSizeLimit]
        [Route("upload")]
        [Obsolete]


        public IActionResult Upload(IFormFile file)
        {
            file = HttpContext.Request.Form.Files["image"];



            if (file.Length > 0)
            {
                var upload = Path.Combine(_hosting.WebRootPath, "images");
                var filename = file.FileName;
                var fullpath = Path.Combine(upload, filename);

                //var fullpath = Path.Combine(@"E:\test mvc\front_end\my-front\src\assets\movie", filename);



                file.CopyTo(new FileStream(fullpath, FileMode.Create));
                return Ok(file.FileName);
            }

            return BadRequest("not saved");

        }



        //[DisableRequestSizeLimit]
        //[Route("upload")]
        //[Obsolete]

        //public IActionResult Upload(IFormFile file)
        //{
        //    file = HttpContext.Request.Form.Files["image"];
        //    var filename = file.FileName;


        //    if (file.Length > 0)
        //    {

        //        WebClient client = new WebClient();
        //        string myFile = @"D:\test_file.txt";
        //        client.Credentials = CredentialCache.DefaultCredentials;
        //        client.UploadFile(@"http://lolo139-001-site1.etempurl.com/assets/movie/", "POST", filename);
        //        client.Dispose();
        //    }

        //    return BadRequest("not saved");

        //}




    }
}
