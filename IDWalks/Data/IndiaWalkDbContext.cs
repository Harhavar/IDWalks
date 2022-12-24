using IDWalks.Models.Domines;
using Microsoft.EntityFrameworkCore;

namespace IDWalks.Data
{

    public class IndiaWalkDbContext : DbContext
    {
        public IndiaWalkDbContext(DbContextOptions<IndiaWalkDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.RoleId);
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.Userid);

        }

        

        public DbSet<Region> regions { get; set; }
        public DbSet<Walk> walks { get; set; }
        public DbSet<WalkDeficulty> walkDeficulties { get; set; }

        public DbSet<User> users { get; set; }

        public DbSet<Role> roles { get; set; }

        public DbSet<User_Role> usersRoles { get; set; }

    }
}
