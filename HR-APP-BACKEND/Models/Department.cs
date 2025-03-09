namespace HR_APP_BACKEND.Models
{
    public class Department
    {
        public int DepartmentId { get; set; } // Primary Key
        public string DepartmentName { get; set; } // e.g., "IT", "Finance"

        // Navigation properties
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}