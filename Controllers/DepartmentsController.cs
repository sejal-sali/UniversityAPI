using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityAPI.Context;
using UniversityAPI.DTOs.UniversityAPI.DTOs;
using UniversityAPI.Models;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentContext _context;

        public DepartmentsController(DepartmentContext context)
        {
            _context = context;
        }

        [HttpGet("GetDepartmentIds")]
        public async Task<ActionResult<IEnumerable<DepartmentIdNameDTO>>> GetDepartmentIds()
        {
            try
            {
                var departmentIdNames = await _context.Department
                    .Select(d => new DepartmentIdNameDTO
                    {
                        DepartmentId = d.DepartmentId,
                        DepartmentName = d.DepartmentName
                    })
                    .ToListAsync();

                return Ok(departmentIdNames);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Log(ex.Message);
                return StatusCode(500, "An error occurred while processing your request." + ex.ToString());
            }
        }


        // GET: api/Departments
        [HttpGet("GetDepartments")]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartments()
        {
            var departments = await _context.Department
                .Select(d => new DepartmentDTO
                {
                    DepartmentName = d.DepartmentName,
                    DepartmentHead = d.DepartmentHead
                })
                .ToListAsync();

            return departments;
        }

        // GET: api/Departments/5
        [HttpGet("GetDepartmentBy{id}")]
        public async Task<ActionResult<DepartmentDetailsDTO>> GetDepartment(int id)
        {
            var department = await _context.Department
                .Where(d => d.DepartmentId == id)
                .Include(d => d.University) // Assuming there is a navigation property 'University' in Department
                .Select(d => new DepartmentDetailsDTO
                {
                    DepartmentName = d.DepartmentName,
                    DepartmentHead = d.DepartmentHead,
                    UniversityName = d.University.UniversityName // Include other department-related properties as needed
                })
                .FirstOrDefaultAsync();

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Departments/5
        [HttpPut("UpdateDepartmentBy{id}")]
        public async Task<IActionResult> PutDepartment(int id, DepartmentUpdateDTO departmentDTO)
        {
            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            // Update department properties with values from DepartmentUpdateDTO
            department.DepartmentName = departmentDTO.DepartmentName;
            department.DepartmentHead = departmentDTO.DepartmentHead;
            // Update other department properties as needed

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        


        [HttpPost("AddDepartment")]
        public async Task<ActionResult<DepartmentDTO>> CreateDepartment(DepartmentCreateDTO departmentDTO)
        {
            try
            {
                // Check if the specified UniversityId exists
                var university = await _context.University.FindAsync(departmentDTO.UniversityId);
                if (university == null)
                {
                    return BadRequest("UniversityId does not exist.");
                }

                // Generate a unique DepartmentId

                var department = new Department
                {
                    DepartmentName = departmentDTO.DepartmentName,
                    DepartmentHead = departmentDTO.DepartmentHead,
                    UniversityId = departmentDTO.UniversityId
                };

                _context.Department.Add(department);
                await _context.SaveChangesAsync();

                var createdDepartmentDTO = new DepartmentDTO
                {
                    DepartmentName = department.DepartmentName,
                    DepartmentHead = department.DepartmentHead
                };

                return CreatedAtAction(nameof(CreateDepartment), createdDepartmentDTO);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Log(ex.Message);
                return StatusCode(500, "An error occurred while processing your request." + ex.ToString());
            }
        }





        // DELETE: api/Departments/5
        [HttpDelete("DeleteDepartmentBy{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (_context.Department == null)
            {
                return NotFound();
            }
            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentExists(int id)
        {
            return (_context.Department?.Any(e => e.DepartmentId == id)).GetValueOrDefault();
        }
    }
}
