using HR_APP_BACKEND.Data;
using HR_APP_BACKEND.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Route("api/course-applications")]
[ApiController]
public class CourseApplicationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CourseApplicationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetApplications()
    {
        var applications = _context.CourseApplications
            .Include(ca => ca.Applicant)
            .Include(ca => ca.Course)
            .ToList();
        return Ok(applications);
    }

    [HttpPut("{id}/approve")]
    public IActionResult ApproveApplication(int id)
    {
        var application = _context.CourseApplications.Find(id);
        if (application == null) return NotFound();
        application.Status = "Approved";
        application.ReviewedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
        application.ReviewedDate = DateTime.UtcNow;

        // Issue certification
        var certification = new Certification
        {
            UserId = application.UserId,
            CourseId = application.CourseId,
            CertificationName = _context.Courses.Find(application.CourseId).CertificationName,
            IssueDate = DateTime.UtcNow
        };
        _context.Certifications.Add(certification);

        _context.SaveChanges();
        return Ok();
    }

    [HttpPut("{id}/reject")]
    public IActionResult RejectApplication(int id)
    {
        var application = _context.CourseApplications.Find(id);
        if (application == null) return NotFound();
        application.Status = "Rejected";
        application.ReviewedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
        application.ReviewedDate = DateTime.UtcNow;
        _context.SaveChanges();
        return Ok();
    }
}