using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAPI.Models
{
    public class Course
    {
        [Key]
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string? CourseName { get; set; }
        [Required]
        public int Credit_Hours { get; set; }
        public string? Description { get; set; }
        [Required]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}
