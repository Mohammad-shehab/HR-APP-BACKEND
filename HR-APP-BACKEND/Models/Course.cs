namespace HR_APP_BACKEND.Models
{
    public class Course
    {
        public int CourseId { get; set; } 
        public string CourseName { get; set; } 
        public string Description { get; set; } 
        public int DepartmentId { get; set; } 
        public string Duration { get; set; } 
        public string CertificationName { get; set; } // Name of certification earned

        // Navigation properties
        public virtual Department? Department { get; set; } // Related department
        public virtual ICollection<CourseApplication>? CourseApplications { get; set; } // Applications for this course
        public virtual ICollection<Certification>? Certifications { get; set; } // Certifications tied to this course
    }
}