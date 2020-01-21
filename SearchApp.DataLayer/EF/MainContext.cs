using Microsoft.EntityFrameworkCore;
using SearchApp.DataLayer.Entities;

namespace SearchApp.DataLayer.EF
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Engine> Engines { get; set; }
    }
}
