using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAPI.Models
{
    public class Department
    {
        [Key]
        [Required]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Department Name is required.")]
        public string? DepartmentName { get; set; }
        public string? DepartmentHead { get; set; }

        [ForeignKey("University")]
        [Required]
        public int UniversityId { get; set; }
        public University University { get; set; }
    }
}
