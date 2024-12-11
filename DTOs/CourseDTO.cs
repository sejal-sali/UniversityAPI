using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.DTOs
{
    // DTO for updating existing course details
    public class CourseUpdateDTO
    {
        [Required(ErrorMessage = "Course Name is required.")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Credit Hours is required.")]
        public int Credit_Hours { get; set; }

        public string Description { get; set; }
    }

    // DTO that includes department details for courses
    public class CourseWithDepartmentDTO
    {
        public string CourseName { get; set; }
        public int Credit_Hours { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentHead { get; set; }
    }

    // DTO for creating a new course
    public class CourseCreateDTO
    {
        [Required(ErrorMessage = "Course Name is required.")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Credit Hours is required.")]
        public int Credit_Hours { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Department ID is required.")]
        public int DepartmentId { get; set; }
    }

    // Simplified DTO for listing basic course information
    public class CourseBasicDTO
    {
        public string CourseName { get; set; }
        public int Credit_Hours { get; set; }
    }

    // Detailed DTO for displaying full course information including department
    public class CourseDetailsDTO
    {
        public string CourseName { get; set; }
        public int Credit_Hours { get; set; }
        public string Description { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentHead { get; set; }
    }
}
