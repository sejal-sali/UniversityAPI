using Microsoft.EntityFrameworkCore;
using UniversityAPI.Models;

namespace UniversityAPI.Context
{
    public class DepartmentContext : DbContext
    {
        public DepartmentContext(DbContextOptions<DepartmentContext> options) : base(options) { }

        public DbSet<Department> Department { get; set; } = null!;
        public DbSet<University> University { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasKey(d => d.DepartmentId); // Setting DepartmentId as the primary key

            modelBuilder.Entity<Department>()
                .Property(d => d.DepartmentName)
                .IsRequired(); // DepartmentName is required

            modelBuilder.Entity<Department>()
                .HasOne(d => d.University) // Department has one University
                .WithMany() // University can have many Departments
                .HasForeignKey(d => d.UniversityId) // Foreign key relationship
                .OnDelete(DeleteBehavior.Restrict); // Define delete behavior if needed

            // If UniversityId is set to required in the database
             modelBuilder.Entity<Department>()
                 .Property(d => d.UniversityId)
                 .IsRequired();

            base.OnModelCreating(modelBuilder);
        }

    }
}