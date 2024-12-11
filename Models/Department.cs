using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAPI.Models
{
    // Represents a department within the university.
    public class Department
    {
        [Key] // Denotes the primary key.
        [Required] 
        public int DepartmentId { get; set; } 

        [Required(ErrorMessage = "Department Name is required.")] 
        public string? DepartmentName { get; set; } // Name of the department.

        public string? DepartmentHead { get; set; } // Optional name of the department head.

        [ForeignKey("University")] // Establishes a foreign key relationship with the University table.
        [Required] 
        public int UniversityId { get; set; } // Foreign key pointing to the associated university.

        public University University { get; set; } // Navigation property to the associated university.
    }
}
