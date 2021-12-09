using Disney.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disney.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieOrSerie> MovieOrSeries { get; set; }
        public DbSet<Character> Characters { get; set; }
    }
}
