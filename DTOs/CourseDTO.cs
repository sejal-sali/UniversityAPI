using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.DTOs
{
    public class CourseUpdateDTO
    {
        [Required(ErrorMessage = "Course Name is required.")]
        public string CourseName { get; set; }
        [Required(ErrorMessage = "Credit Hours is required.")]
        public int Credit_Hours { get; set; }
        public string Description { get; set; }
    }
    public class CourseWithDepartmentDTO
    {
        public string CourseName { get; set; }
        public int Credit_Hours { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentHead { get; set; }
    }
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

    public class CourseBasicDTO
    {
        public string CourseName { get; set; }
        public int Credit_Hours { get; set; }
    }
    public class CourseDetailsDTO
    {
        public string CourseName { get; set; }
        public int Credit_Hours { get; set; }
        public string Description { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentHead { get; set; }
    }
}
