namespace HR_APP_BACKEND.Models
{
    public class Course
    {
        public int CourseId { get; set; } // Primary Key
        public string CourseName { get; set; } // e.g., "Advanced SQL"
        public string Description { get; set; } // Course details
        public int DepartmentId { get; set; } // Foreign Key to Department
        public string Duration { get; set; } // e.g., "2 weeks"
        public string CertificationName { get; set; } // Name of certification earned

        // Navigation properties
        public virtual Department Department { get; set; } // Related department
        public virtual ICollection<CourseApplication> CourseApplications { get; set; } // Applications for this course
        public virtual ICollection<Certification> Certifications { get; set; } // Certifications tied to this course
    }
}