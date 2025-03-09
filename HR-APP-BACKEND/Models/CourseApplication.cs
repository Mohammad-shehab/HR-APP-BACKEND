using System.ComponentModel.DataAnnotations;

namespace HR_APP_BACKEND.Models
{
    public class CourseApplication
    {
        [Key] // Explicitly mark ApplicationId as the primary key
        public int ApplicationId { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public string Status { get; set; }
        public DateTime AppliedDate { get; set; }
        public string? ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public virtual ApplicationUser Applicant { get; set; }
        public virtual Course Course { get; set; }
        public virtual ApplicationUser Reviewer { get; set; }
    }
}