using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaApi.Models
{
    public class ApplicationDbContext:IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Empolyee> Empolyees { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Tv_Show> Tv_Shows { get; set; }
        public DbSet<Shows> Shows { get; set; }
        public DbSet<Category> Categories { get; set; }

      
    }
}
