namespace HR_APP_BACKEND.Models
{
    public class Certification
    {
        public int CertificationId { get; set; } // Primary Key
        public string UserId { get; set; } // Foreign Key to ApplicationUser
        public int CourseId { get; set; } // Foreign Key to Course
        public string CertificationName { get; set; } // Name of the certification
        public DateTime IssueDate { get; set; } // Date issued

        // Navigation properties
        public virtual ApplicationUser User { get; set; } // User who earned it
        public virtual Course Course { get; set; } // Course tied to the certification
    }
}