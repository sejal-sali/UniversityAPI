using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Context;
using UniversityAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly UniversityContext _context;

    public CoursesController(UniversityContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
        return await _context.Courses.Include(c => c.Department).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Course>> AddCourse(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCourses), new { id = course.CourseId }, course);
    }
}
