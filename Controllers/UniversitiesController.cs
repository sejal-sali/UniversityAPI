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
    public class UniversitiesController : ControllerBase
    {
        private readonly UniversityContext _context;

        public UniversitiesController(UniversityContext context)
        {
            _context = context;
        }

        // GET: api/Universities
        /// <summary>
        /// Retrieves all universities with their associated departments.
        /// </summary>
        /// <returns>List of universities</returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UniversityDTO>>> GetUniversities()
        {
            return await _context.Universities
                .Include(u => u.Departments)
                .Select(u => new UniversityDTO
                {
                    UniversityId = u.UniversityId,
                    UniversityName = u.Name,
                    Departments = u.Departments.Select(d => new DepartmentDTO
                    {
                        DepartmentId = d.DepartmentId,
                        DepartmentName = d.DepartmentName
                    }).ToList()
                })
                .ToListAsync();
        }

        // POST: api/Universities
        /// <summary>
        /// Adds a new university.
        /// </summary>
        /// <param name="universityDTO">Details of the university to add</param>
        /// <returns>The newly created university</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<University>> AddUniversity(UniversityCreateDTO universityDTO)
        {
            var university = new University
            {
                Name = universityDTO.UniversityName
            };

            _context.Universities.Add(university);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUniversities), new { id = university.UniversityId }, university);
        }

        // DELETE: api/Universities/5
        /// <summary>
        /// Deletes a university by ID.
        /// </summary>
        /// <param name="id">The ID of the university to delete</param>
        /// <returns>No content</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUniversity(int id)
        {
            var university = await _context.Universities.FindAsync(id);
            if (university == null)
            {
                return NotFound();
            }

            _context.Universities.Remove(university);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
