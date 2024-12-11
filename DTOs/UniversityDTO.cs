using System.ComponentModel.DataAnnotations;
using UniversityAPI.Models;

namespace UniversityAPI.DTOs
{
    // DTO for creating a new university record with validation rules for each field.
    public class UniversityCreateDTO
    {
        [Required(ErrorMessage = "University Name is required.")]
        public string UniversityName { get; set; }

        [Required(ErrorMessage = "Establishment year is required.")]
        [ValidYear(ErrorMessage = "Please enter a valid year.")]
        public string EstablishmentYear { get; set; }

        [StringLength(50)]
        public string UniversityType { get; set; }

        public string Contact { get; set; }
        public string Location { get; set; }
    }

    // DTO for updating existing university information, applying similar validation rules.
    public class UniversityUpdateDTO
    {
        [Required(ErrorMessage = "University Name is required.")]
        public string UniversityName { get; set; }

        [StringLength(50)]
        public string UniversityType { get; set; }

        public string Contact { get; set; }
        public string Location { get; set; }
    }

    // Simple DTO for basic university information.
    public class UniversityDTO
    {
        public string UniversityName { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }
    }

    // DTO for listing universities by ID and name.
    public class UniversityIdNameDTO
    {
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
    }

    // Detailed DTO for full information about a university, suitable for display purposes.
    public class UniversityDTODetails
    {
        public string UniversityName { get; set; }
        public string EstablishmentYear { get; set; }
        public string UniversityType { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }
    }

    // Custom validation attribute to ensure a year is valid and not in the future.
    public class ValidYearAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !int.TryParse(value.ToString(), out int year))
            {
                return new ValidationResult(ErrorMessage);
            }

            int currentYear = DateTime.Now.Year;

            // Validates that the year is not in the future.
            if (year <= currentYear)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
