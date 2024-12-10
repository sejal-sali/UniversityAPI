using Microsoft.EntityFrameworkCore;
using UniversityAPI.Models;

namespace UniversityAPI.Context
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) { }

        public DbSet<University> University { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define any custom configurations or relationships here
            modelBuilder.Entity<University>().HasKey(u => u.UniversityId); // Sets UniversityId as the primary key
            modelBuilder.Entity<University>().Property(u => u.UniversityName).IsRequired(); // UniversityName is required

            base.OnModelCreating(modelBuilder);
        }

    }
}