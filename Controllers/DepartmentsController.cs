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
    public class DepartmentsController : ControllerBase
    {
        private readonly UniversityContext _context;

        public DepartmentsController(UniversityContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        /// <summary>
        /// Retrieves all departments with their associated courses.
        /// </summary>
        /// <returns>List of departments</returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartments()
        {
            return await _context.Departments
                .Include(d => d.Courses)
                .Select(d => new DepartmentDTO
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    Courses = d.Courses.Select(c => new CourseDTO
                    {
                        CourseId = c.CourseId,
                        CourseName = c.CourseName
                    }).ToList()
                })
                .ToListAsync();
        }

        // POST: api/Departments
        /// <summary>
        /// Adds a new department.
        /// </summary>
        /// <param name="departmentDTO">Details of the department to add</param>
        /// <returns>The newly created department</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Department>> AddDepartment(DepartmentCreateDTO departmentDTO)
        {
            var department = new Department
            {
                DepartmentName = departmentDTO.DepartmentName,
                UniversityId = departmentDTO.UniversityId
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartments), new { id = department.DepartmentId }, department);
        }

        // DELETE: api/Departments/5
        /// <summary>
        /// Deletes a department by ID.
        /// </summary>
        /// <param name="id">The ID of the department to delete</param>
        /// <returns>No content</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
