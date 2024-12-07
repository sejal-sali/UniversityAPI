using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.Models
{
    public class University
    {
        [Key]
        public int UniversityId { get; set; }

        [Required]
        public string UniversityName { get; set; } = string.Empty;

        public string? Contact { get; set; }
        public string? Location { get; set; }
        public int EstablishmentYear { get; set; }
        public string? UniversityType { get; set; }

        public ICollection<Department>? Departments { get; set; }
    }
}
