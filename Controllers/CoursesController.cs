using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityAPI.Context;
using UniversityAPI.Models;
using UniversityAPI.DTOs;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CourseContext _context;

        public CoursesController(CourseContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet("GetCourses")]
        public async Task<ActionResult<IEnumerable<CourseBasicDTO>>> GetCourses()
        {
            var courses = await _context.Course
                .Select(c => new CourseBasicDTO
                {
                    CourseName = c.CourseName,
                    Credit_Hours = c.Credit_Hours
                })
                .ToListAsync();

            return courses;
        }

        // GET: api/Courses/5
        [HttpGet("GetCourseById{id}")]
        public async Task<ActionResult<CourseDetailsDTO>> GetCourse(int id)
        {
            var course = await _context.Course
                .Include(c => c.Department)
                .Where(c => c.CourseId == id)
                .Select(c => new CourseDetailsDTO
                {
                    CourseName = c.CourseName,
                    Credit_Hours = c.Credit_Hours,
                    Description = c.Description,
                    DepartmentName = c.Department.DepartmentName,
                    DepartmentHead = c.Department.DepartmentHead
                })
                .FirstOrDefaultAsync();

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }


        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateCourseBy{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseUpdateDTO updateCourseDTO)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            // Update course properties with values from CourseUpdateDTO
            course.CourseName = updateCourseDTO.CourseName;
            course.Credit_Hours = updateCourseDTO.Credit_Hours;
            course.Description = updateCourseDTO.Description;

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // GET: api/Courses/WithDepartment
        [HttpGet("GetCoursesWithDepartment")]
        public async Task<ActionResult<IEnumerable<CourseWithDepartmentDTO>>> GetCoursesWithDepartment()
        {
            var coursesWithDepartment = await _context.Course
                .Include(c => c.Department)
                .Select(c => new CourseWithDepartmentDTO
                {
                    CourseName = c.CourseName,
                    Credit_Hours = c.Credit_Hours,
                    DepartmentName = c.Department.DepartmentName,
                    DepartmentHead = c.Department.DepartmentHead
                })
                .ToListAsync();

            return coursesWithDepartment;
        }


        

        // POST: api/Courses
        [HttpPost("AddCourse")]
        public async Task<ActionResult<Course>> PostCourse(CourseCreateDTO courseDTO)
        {
            var department = await _context.Department.FindAsync(courseDTO.DepartmentId);
            if (department == null)
            {
                return BadRequest("Department not found.");
            }

            var course = new Course
            {
                CourseName = courseDTO.CourseName,
                Credit_Hours = courseDTO.Credit_Hours,
                Description = courseDTO.Description,
                DepartmentId = courseDTO.DepartmentId
            };

            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }


        // DELETE: api/Courses/5
        [HttpDelete("DeleteCourseBy{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (_context.Course == null)
            {
                return NotFound();
            }
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return (_context.Course?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }
    }
}
