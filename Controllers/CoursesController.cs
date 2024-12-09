namespace UniversityAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using UniversityAPI.Context;
    using UniversityAPI.DTOs;
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

        // GET: api/Courses
        /// <summary>
        /// Retrieves all courses with their associated department.
        /// </summary>
        /// <returns>List of courses</returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            return await _context.Courses
                .Include(c => c.Department)
                .Select(c => new CourseDTO
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    DepartmentName = c.Department!.DepartmentName
                })
                .ToListAsync();
        }

        // POST: api/Courses
        /// <summary>
        /// Adds a new course.
        /// </summary>
        /// <param name="courseDTO">Details of the course to add</param>
        /// <returns>The newly created course</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Course>> AddCourse(CourseCreateDTO courseDTO)
        {
            var course = new Course
            {
                CourseName = courseDTO.CourseName,
                DepartmentId = courseDTO.DepartmentId
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourses), new { id = course.CourseId }, course);
        }

        // DELETE: api/Courses/5
        /// <summary>
        /// Deletes a course by ID.
        /// </summary>
        /// <param name="id">The ID of the course to delete</param>
        /// <returns>No content</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
