using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; } = string.Empty;

        public string? DepartmentHead { get; set; }

        public int UniversityId { get; set; }
        public University? University { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}
