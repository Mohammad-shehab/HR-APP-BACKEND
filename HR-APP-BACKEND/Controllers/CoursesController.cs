using HR_APP_BACKEND.Data;
using HR_APP_BACKEND.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/courses")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CoursesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetCourses([FromQuery] int? departmentId)
    {
        var courses = _context.Courses
            .Where(c => departmentId == null || c.DepartmentId == departmentId)
            .ToList();
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public IActionResult GetCourse(int id)
    {
        var course = _context.Courses.Find(id);
        if (course == null) return NotFound();
        return Ok(course);
    }

    [HttpPost("apply")]
    public IActionResult ApplyForCourse([FromBody] ApplyModel model)
    {
        var application = new CourseApplication
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), // Current user ID from JWT
            CourseId = model.CourseId,
            Status = "Pending",
            AppliedDate = DateTime.UtcNow
        };
        _context.CourseApplications.Add(application);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPost]
    //[Authorize(Roles = "HR")]
    public IActionResult AddCourse([FromBody] Course newCourse)
    {
        _context.Courses.Add(newCourse);
        _context.SaveChanges();
        return Ok(newCourse);
    }


}

public class ApplyModel
{
    public int CourseId { get; set; }
}