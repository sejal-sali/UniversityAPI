using System.ComponentModel.DataAnnotations;
namespace UniversityAPI.DTOs
{
    public class CourseDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
    }

    public class CourseCreateDTO
    {
        [Required]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }
    }
}
