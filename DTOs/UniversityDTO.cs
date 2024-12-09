using System.ComponentModel.DataAnnotations;
namespace UniversityAPI.DTOs
{
    public class UniversityDTO
    {
        public int UniversityId { get; set; }
        public string UniversityName { get; set; } = string.Empty;
        public List<DepartmentDTO> Departments { get; set; } = new List<DepartmentDTO>();
    }

    public class UniversityCreateDTO
    {
        [Required]
        public string UniversityName { get; set; } = string.Empty;
    }
}