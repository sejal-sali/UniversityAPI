using Microsoft.EntityFrameworkCore;
using UniversityAPI.Models;

namespace UniversityAPI.Context
{
    public class CourseContext : DbContext
    {
        public CourseContext(DbContextOptions<CourseContext> options) : base(options) { }

        public DbSet<Course> Course { get; set; } = null!;
        public DbSet<Department> Department { get; set; }
    }
}