using HR_APP_BACKEND.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("me")]
    public IActionResult GetProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = _context.Users
            .Include(u => u.CourseApplications)
            .ThenInclude(ca => ca.Course)
            .Include(u => u.Certifications)
            .FirstOrDefault(u => u.Id == userId);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpGet]
    [Authorize(Roles = "HR")] // HR only
    public IActionResult GetAllEmployees()
    {
        var employees = _context.Users
            .Where(u => u.Role == "Employee")
            .Include(u => u.CourseApplications)
                .ThenInclude(ca => ca.Course)
            .Include(u => u.Certifications)
                .ThenInclude(cert => cert.Course)
            .ToList();

        return Ok(employees);
    }

}