using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.DTOs
{
    namespace UniversityAPI.DTOs
    {
        public class DepartmentCreateDTO
        {
            [Required(ErrorMessage = "Department Name is required.")]
            public string DepartmentName { get; set; }
            public string DepartmentHead { get; set; }

            [Required(ErrorMessage = "University ID is required.")]
            public int UniversityId { get; set; }
        }
        public class DepartmentIdNameDTO
        {
            public int DepartmentId { get; set; }
            public string DepartmentName { get; set; }
        }
        public class DepartmentDTO
        {
            public string? DepartmentName { get; set; }
            public string? DepartmentHead { get; set; }
        }

        public class DepartmentDetailsDTO
        {
            public string DepartmentName { get; set; }
            public string DepartmentHead { get; set; }
            public string UniversityName { get; set; }
            
        }

        public class DepartmentUpdateDTO
        {
            [Required(ErrorMessage = "Department Name is required.")]
            public string DepartmentName { get; set; }
            public string DepartmentHead { get; set; }

        }
       
    }

}
