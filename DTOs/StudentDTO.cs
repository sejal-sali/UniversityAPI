namespace UniversityAPI.DTOs
{
    public class StudentDTO
    {
        public int StudentId { get; set; }
        public string? Name { get; set; }

        public string? Email { get; set; }
        public string? DepartmentName { get; set; }

        

    }

    public class StudentCreateDTO
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public int DepartmentId { get; set; }
    }
}
