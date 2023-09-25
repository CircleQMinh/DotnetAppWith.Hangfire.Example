using DotnetAppWith.Hangfire.Example.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetAppWith.Hangfire.Example.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public AppDbContext() : base()
        {

        }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
