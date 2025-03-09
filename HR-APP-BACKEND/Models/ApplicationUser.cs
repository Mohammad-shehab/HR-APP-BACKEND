using Microsoft.AspNetCore.Identity;

namespace HR_APP_BACKEND.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Role to distinguish between Employee and HR
        public string Role { get; set; } // "Employee" or "HR"

        // Additional user details
        public string FullName { get; set; }

        // Foreign Key to Department (nullable for HR)
        public int? DepartmentId { get; set; }

        // Navigation property for Department
        public virtual Department Department { get; set; }

        // Navigation properties for related entities
        public virtual ICollection<CourseApplication> CourseApplications { get; set; } // Applications submitted by this user
        public virtual ICollection<CourseApplication> ReviewedApplications { get; set; } // Applications reviewed by this user (HR)
        public virtual ICollection<Certification> Certifications { get; set; } // Certifications earned by this user
    }
}