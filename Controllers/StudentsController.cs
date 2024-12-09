using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityAPI.Context;
using UniversityAPI.DTOs;
using UniversityAPI.Models;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly UniversityContext _context;

        public StudentsController(UniversityContext context)
        {
            _context = context;
        }

        // GET: api/Students
        /// <summary>
        /// Retrieves all students.
        /// </summary>
        /// <returns>List of students with department names</returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            return await _context.Students
                .Include(s => s.Department)
                .Select(s => new StudentDTO
                {
                    StudentId = s.StudentId,
                    Name = s.Name,
                    Email = s.Email,
                    DepartmentName = s.Department!.DepartmentName
                })
                .ToListAsync();
        }

        // GET: api/Students/5
        /// <summary>
        /// Retrieves a specific student by ID.
        /// </summary>
        /// <param name="id">The ID of the student.</param>
        /// <returns>A student with detailed information</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
        {
            var student = await _context.Students
                .Include(s => s.Department)
                .Where(s => s.StudentId == id)
                .Select(s => new StudentDTO
                {
                    StudentId = s.StudentId,
                    Name = s.Name,
                    Email = s.Email,
                    DepartmentName = s.Department!.DepartmentName
                })
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // POST: api/Students
        /// <summary>
        /// Adds a new student.
        /// </summary>
        /// <param name="studentDTO">Details of the student to add</param>
        /// <returns>Created student details</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentCreateDTO studentDTO)
        {
            var student = new Student
            {
                Name = studentDTO.Name,
                Email = studentDTO.Email,
                DepartmentId = studentDTO.DepartmentId
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
        }

        // PUT: api/Students/5
        /// <summary>
        /// Updates an existing student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to update</param>
        /// <param name="studentDTO">Updated student details</param>
        /// <returns>No content</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, StudentCreateDTO studentDTO)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            student.Name = studentDTO.Name;
            student.Email = studentDTO.Email;
            student.DepartmentId = studentDTO.DepartmentId;

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // DELETE: api/Students/5
        /// <summary>
        /// Deletes a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to delete</param>
        /// <returns>No content</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Checks if a student exists.
        /// </summary>
        /// <param name="id">The ID of the student</param>
        /// <returns>True if student exists, false otherwise</returns>
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
