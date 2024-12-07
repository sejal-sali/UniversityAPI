using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Context;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
    {
        return await _context.Departments.Include(d => d.Courses).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Department>> AddDepartment(Department department)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDepartments), new { id = department.DepartmentId }, department);
    }
}
