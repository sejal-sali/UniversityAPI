using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Email { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
