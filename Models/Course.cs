using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAPI.Models
{
    // Represents a Course entity within the database.
    public class Course
    {
        [Key]  // Denotes the primary key.
        [Required]  
        public int CourseId { get; set; }  // Unique identifier for the course.

        [Required]  
        public string? CourseName { get; set; }  // Name of the course.

        [Required]  
        public int Credit_Hours { get; set; }  // Number of credit hours the course carries.

        public string? Description { get; set; }  // Optional description of the course.

        [Required]  
        [ForeignKey("Department")]  // Establishes a foreign key relationship to the Department table.
        public int DepartmentId { get; set; }  // Foreign key linking to the corresponding department.

        public Department Department { get; set; }  // Navigation property to the associated department.
    }
}
