using HR_APP_BACKEND.Data;
using HR_APP_BACKEND.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HR_APP_BACKEND.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/courses?departmentId=1
        [HttpGet]
        [AllowAnonymous] // Public access for employees to view courses
        public IActionResult GetCourses([FromQuery] int? departmentId)
        {
            var courses = _context.Courses
                .Where(c => departmentId == null || c.DepartmentId == departmentId)
                .ToList();
            return Ok(courses);
        }

        // GET: api/courses/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        // POST: api/courses/apply
        [HttpPost("apply")]
        [Authorize] // Requires logged-in user (employee or HR)
        public IActionResult ApplyForCourse([FromBody] ApplyModel model)
        {
            if (!ModelState.IsValid || model.CourseId <= 0)
                return BadRequest("Invalid CourseId.");

            var courseExists = _context.Courses.Any(c => c.CourseId == model.CourseId);
            if (!courseExists)
                return NotFound("Course not found.");

            var application = new CourseApplication
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), // Current user ID from JWT
                CourseId = model.CourseId,
                // Status defaults to "Pending" via DbContext, no need to set here
                AppliedDate = DateTime.UtcNow
            };
            _context.CourseApplications.Add(application);
            _context.SaveChanges();
            return Ok(new { ApplicationId = application.ApplicationId });
        }

        // POST: api/courses (Add a new course)
        [HttpPost]
        [Authorize(Roles = "HR")] // Only HR can add courses
        public IActionResult AddCourse([FromBody] Course newCourse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validate required fields
            if (string.IsNullOrWhiteSpace(newCourse.CourseName) || newCourse.DepartmentId <= 0)
                return BadRequest("CourseName and DepartmentId are required.");

            // Check if DepartmentId exists
            var departmentExists = _context.Departments.Any(d => d.DepartmentId == newCourse.DepartmentId);
            if (!departmentExists)
                return BadRequest("Invalid DepartmentId.");

            // Ensure CourseId is not set (auto-incremented by database)
            newCourse.CourseId = 0;

            _context.Courses.Add(newCourse);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCourse), new { id = newCourse.CourseId }, newCourse);
        }

        // DELETE: api/courses/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "HR")] // Only HR can delete courses
        public IActionResult DeleteCourse(int id)
        {
            var course = _context.Courses
                .Include(c => c.CourseApplications) // Check for related applications
                .Include(c => c.Certifications)     // Check for related certifications
                .FirstOrDefault(c => c.CourseId == id);

            if (course == null)
                return NotFound("Course not found.");

            // Prevent deletion if there are related applications or certifications
            if (course.CourseApplications.Any() || course.Certifications.Any())
                return BadRequest("Cannot delete course with existing applications or certifications.");

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return NoContent();
        }
    }

    public class ApplyModel
    {
        public int CourseId { get; set; }
    }
}

