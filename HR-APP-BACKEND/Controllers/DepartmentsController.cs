using HR_APP_BACKEND.Data;
using HR_APP_BACKEND.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_APP_BACKEND.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/departments
        [HttpGet]
        [AllowAnonymous] // Accessible to all (e.g., for dropdown in frontend)
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            var departments = await _context.Departments
                .ToListAsync();
            return Ok(departments);
        }

        // GET: api/departments/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Departments
                .Include(d => d.Courses) // Include related courses
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // GET: api/departments/{id}/courses
        [HttpGet("{id}/courses")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByDepartment(int id)
        {
            var department = await _context.Departments
                .Include(d => d.Courses)
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
            {
                return NotFound("Department not found.");
            }

            return Ok(department.Courses);
        }

        // POST: api/departments
        [HttpPost]
        [Authorize(Roles = "HR")] // Only HR can create departments
        public async Task<ActionResult<Department>> CreateDepartment([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure DepartmentId is not set (auto-incremented by database)
            department.DepartmentId = 0;

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentId }, department);
        }

        // PUT: api/departments/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "HR")] // Only HR can update departments
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest("Department ID mismatch.");
            }

            var existingDepartment = await _context.Departments.FindAsync(id);
            if (existingDepartment == null)
            {
                return NotFound();
            }

            existingDepartment.DepartmentName = department.DepartmentName; // Update only the name
            _context.Entry(existingDepartment).State = EntityState.Modified;

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
                throw;
            }

            return NoContent();
        }

        // DELETE: api/departments/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "HR")] // Only HR can delete departments
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments
                .Include(d => d.Users) // Check for related users
                .Include(d => d.Courses) // Check for related courses
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
            {
                return NotFound();
            }

            // Prevent deletion if there are related users or courses
            if (department.Users.Any() || department.Courses.Any())
            {
                return BadRequest("Cannot delete department with associated users or courses.");
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(d => d.DepartmentId == id);
        }
    }
}