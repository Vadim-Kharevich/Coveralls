using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoverallsWeb.Models
{
    public class CoverallsContext:DbContext
    {
        public DbSet<Coveralls> Coveralls { get; set; }
        public DbSet<Storage> Storage { get; set; }
        public DbSet<CoverallsType> Types { get; set; }

        public CoverallsContext(DbContextOptions<CoverallsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
