using System.ComponentModel.DataAnnotations;
using UniversityAPI.Models;

namespace UniversityAPI.DTOs
{
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

    public class UniversityUpdateDTO
    {
        [Required(ErrorMessage = "University Name is required.")]
        public string UniversityName { get; set; }
        [StringLength(50)]
        public string UniversityType { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }
    }

    public class UniversityDTO
    {
        public string UniversityName { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }

    }
    public class UniversityIdNameDTO
    {
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
    }
    public class UniversityDTODetails
    {
        public string UniversityName { get; set; }
        public string EstablishmentYear { get; set; }
        public string UniversityType { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }

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


