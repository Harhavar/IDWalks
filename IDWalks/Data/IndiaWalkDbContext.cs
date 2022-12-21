using IDWalks.Models.Domines;
using Microsoft.EntityFrameworkCore;

namespace IDWalks.Data
{

    public class IndiaWalkDbContext : DbContext
    {
        public IndiaWalkDbContext(DbContextOptions<IndiaWalkDbContext> options) : base(options)
        {

        }

        public DbSet<Region> regions { get; set; }
        public DbSet<Walk> walks { get; set; }
        public DbSet<WalkDeficulty> walkDeficulties { get; set; }
    }
}
