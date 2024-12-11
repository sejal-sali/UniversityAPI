using System;
using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.Models
{
    // Represents a university entity within the database.
    public class University
    {
        [Key] // the primary key in the database.
        [Required] 
        public int UniversityId { get; set; }

        [Required(ErrorMessage = "University Name is required.")]
        public string? UniversityName { get; set; } // Name of the university.

        public string? Contact { get; set; } // Contact details for the university, optional.

        public string? Location { get; set; } // Location of the university, optional.

        [Required(ErrorMessage = "Establishment year is required.")]
        [ValidYear(ErrorMessage = "Please enter a valid year.")] // Custom validation to ensure the year is not in the future.
        public string? EstablishmentYear { get; set; } // Year the university was established.

        [StringLength(50)] 
        public string? UniversityType { get; set; } 
    }

    // Custom validation attribute to ensure that a provided year is valid and not in the future.
    public class ValidYearAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !int.TryParse(value.ToString(), out int year))
            {
                return new ValidationResult(ErrorMessage);
            }

            int currentYear = DateTime.Now.Year;

            // Check if the year is greater than the current year.
            if (year <= currentYear)
            {
                return ValidationResult.Success; // Year is valid.
            }

            return new ValidationResult(ErrorMessage); // Year is in the future, thus invalid.
        }
    }
}
