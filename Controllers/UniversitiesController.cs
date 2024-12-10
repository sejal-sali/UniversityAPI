using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityAPI.Context;
using UniversityAPI.DTOs;
using UniversityAPI.Models;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private readonly UniversityContext _context;

        public UniversitiesController(UniversityContext context)
        {
            _context = context;
            //context.Database.EnsureCreated();
        }

        [HttpGet("GetUniversityIds")]
        public async Task<ActionResult<IEnumerable<UniversityIdNameDTO>>> GetUniversityIds()
        {
            try
            {
                var universityIdNames = await _context.University
                    .Select(u => new UniversityIdNameDTO
                    {
                        UniversityId = u.UniversityId,
                        UniversityName = u.UniversityName
                    })
                    .ToListAsync();

                return Ok(universityIdNames);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Log(ex.Message);
                return StatusCode(500, "An error occurred while processing your request." + ex.ToString());
            }
        }


        // GET: api/Universities
        [HttpGet("GetUniversities")]
        public async Task<ActionResult<IEnumerable<UniversityDTO>>> GetUniversities()
        {
            var universities = await _context.University
                .Select(u => new UniversityDTO
                {
                    UniversityName = u.UniversityName,
                    Contact = u.Contact,
                    Location = u.Location
                })
                .ToListAsync();

            return universities;
        }

        // GET: api/Universities/5
        [HttpGet("GetUniversityBy{id}")]
        public async Task<ActionResult<UniversityDTODetails>> GetUniversity(int id)
        {
            var university = await _context.University
                .Where(u => u.UniversityId == id)
                .Select(u => new UniversityDTODetails
                {
                    UniversityName = u.UniversityName,
                    EstablishmentYear = u.EstablishmentYear,
                    UniversityType = u.UniversityType,
                    Contact = u.Contact,
                    Location = u.Location
                })
                .FirstOrDefaultAsync();

            if (university == null)
            {
                return NotFound();
            }

            return university;
        }

        // PUT: api/Universities/5
        [HttpPut("UpdateUniversityBy{id}")]
        public async Task<IActionResult> PutUniversity(int id, UniversityUpdateDTO universityDTO)
        {
            var university = await _context.University.FindAsync(id);
            if (university == null)
            {
                return NotFound();
            }

            // Update university properties with values from UniversityUpdateDTO
            university.UniversityName = universityDTO.UniversityName;
            university.UniversityType = universityDTO.UniversityType;
            university.Contact = universityDTO.Contact;
            university.Location = universityDTO.Location;

            _context.Entry(university).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UniversityExists(id))
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

        // POST: api/Universities
        [HttpPost("AddUniversity")]
        public async Task<ActionResult<University>> PostUniversity(UniversityCreateDTO universityDTO)
        {
            var university = new University
            {
                UniversityName = universityDTO.UniversityName,
                EstablishmentYear = universityDTO.EstablishmentYear,
                UniversityType = universityDTO.UniversityType,
                Contact = universityDTO.Contact,
                Location = universityDTO.Location
            };

            _context.University.Add(university);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUniversity), new { id = university.UniversityId }, university);
        }


        // DELETE: api/Universities/5
        [HttpDelete("DeleteUniversityBy{id}")]
        public async Task<IActionResult> DeleteUniversity(int id)
        {
            if (_context.University == null)
            {
                return NotFound();
            }
            var university = await _context.University.FindAsync(id);
            if (university == null)
            {
                return NotFound();
            }

            _context.University.Remove(university);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UniversityExists(int id)
        {
            return (_context.University?.Any(e => e.UniversityId == id)).GetValueOrDefault();
        }
    }
}