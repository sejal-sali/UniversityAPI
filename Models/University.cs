using System;
using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.Models
{
    public class University
    {
        [Key]
        [Required]
        public int UniversityId { get; set; }

        [Required(ErrorMessage = "University Name is required.")]
        public string? UniversityName { get; set; }

        public string? Contact { get; set; }

        public string? Location { get; set; }

        [Required(ErrorMessage = "Establishment year is required.")]
        [ValidYear(ErrorMessage = "Please enter a valid year.")]
        public string? EstablishmentYear { get; set; }

        [StringLength(50)] // For example, set maximum length as needed
        public string? UniversityType { get; set; }
    }

    public class ValidYearAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !int.TryParse(value.ToString(), out int year))
            {
                return new ValidationResult(ErrorMessage);
            }

            int currentYear = DateTime.Now.Year;

            // Assuming establishments should not be in the future.
            if (year <= currentYear)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
