using System.ComponentModel.DataAnnotations;
namespace UniversityAPI.DTOs
{

    public class DepartmentDTO
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public List<CourseDTO> Courses { get; set; } = new List<CourseDTO>();
    }

    public class DepartmentCreateDTO
    {
        [Required]
        public string DepartmentName { get; set; } = string.Empty;

        [Required]
        public int UniversityId { get; set; }
    }
}
