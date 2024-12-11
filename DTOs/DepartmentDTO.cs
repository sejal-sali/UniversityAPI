using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.DTOs
{
    // DTO for creating a new department
    public class DepartmentCreateDTO
    {
        [Required(ErrorMessage = "Department Name is required.")]
        public string DepartmentName { get; set; }

        // Optional property for the head of the department
        public string DepartmentHead { get; set; }

        [Required(ErrorMessage = "University ID is required.")]
        public int UniversityId { get; set; }
    }

    // DTO for retrieving department id and name
    public class DepartmentIdNameDTO
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

    // Simple DTO for general department information
    public class DepartmentDTO
    {
        public string? DepartmentName { get; set; }
        public string? DepartmentHead { get; set; }
    }

    // DTO for detailed department information
    public class DepartmentDetailsDTO
    {
        public string DepartmentName { get; set; }
        public string DepartmentHead { get; set; }
        public string UniversityName { get; set; }
    }

    // DTO for updating existing department information
    public class DepartmentUpdateDTO
    {
        [Required(ErrorMessage = "Department Name is required.")]
        public string DepartmentName { get; set; }
        public string DepartmentHead { get; set; }
    }
}
