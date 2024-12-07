using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; } = string.Empty;

        public int CreditHours { get; set; }
        public string? Description { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
