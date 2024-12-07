using Microsoft.AspNetCore.Mvc;
using System.Net;
using UniversityAPI.Context;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<University>>> GetUniversities()
    {
        return await _context.Universities.Include(u => u.Departments).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<University>> AddUniversity(University university)
    {
        _context.Universities.Add(university);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUniversities), new { id = university.UniversityId }, university);
    }

    [Fact]
    public async Task GetUniversities_ReturnsOkResult()
    {
        var response = await _client.GetAsync("/api/Universities");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

}
