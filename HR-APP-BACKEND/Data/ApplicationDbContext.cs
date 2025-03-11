using HR_APP_BACKEND.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HR_APP_BACKEND.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseApplication> CourseApplications { get; set; }
        public DbSet<Certification> Certifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define primary key for CourseApplication
            modelBuilder.Entity<CourseApplication>()
                .HasKey(ca => ca.ApplicationId);

            // Configure relationships
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId)
                .IsRequired(false);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Users)
                .WithOne(u => u.Department)
                .HasForeignKey(u => u.DepartmentId);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Courses)
                .WithOne(c => c.Department)
                .HasForeignKey(c => c.DepartmentId);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.CourseApplications)
                .WithOne(ca => ca.Course)
                .HasForeignKey(ca => ca.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Certifications)
                .WithOne(cert => cert.Course)
                .HasForeignKey(cert => cert.CourseId);

            modelBuilder.Entity<CourseApplication>()
                .HasOne(ca => ca.Applicant)
                .WithMany(u => u.CourseApplications)
                .HasForeignKey(ca => ca.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseApplication>()
                .HasOne(ca => ca.Reviewer)
                .WithMany(u => u.ReviewedApplications)
                .HasForeignKey(ca => ca.ReviewedBy)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseApplication>()
                .HasOne(ca => ca.Course)
                .WithMany(c => c.CourseApplications)
                .HasForeignKey(ca => ca.CourseId);

            modelBuilder.Entity<Certification>()
                .HasOne(cert => cert.User)
                .WithMany(u => u.Certifications)
                .HasForeignKey(cert => cert.UserId);

            modelBuilder.Entity<Certification>()
                .HasOne(cert => cert.Course)
                .WithMany(c => c.Certifications)
                .HasForeignKey(cert => cert.CourseId);

            // Configure CourseApplication properties
            modelBuilder.Entity<CourseApplication>()
                .Property(ca => ca.Status)
                .HasDefaultValue("Pending"); // Already here, sets default status

            modelBuilder.Entity<CourseApplication>()
                .Property(ca => ca.AppliedDate)
                .HasDefaultValueSql("GETDATE()");

            // NEW: Add CompletionDate as an optional field (nullable)
            modelBuilder.Entity<CourseApplication>()
                .Property(ca => ca.CompletionDate)
                .IsRequired(false); // Optional, only set when course is completed

            // Seed Department data
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "IT" },
                new Department { DepartmentId = 2, DepartmentName = "Finance" },
                new Department { DepartmentId = 3, DepartmentName = "Human Resources" },
                new Department { DepartmentId = 4, DepartmentName = "Marketing" },
                new Department { DepartmentId = 5, DepartmentName = "Operations" },
                new Department { DepartmentId = 6, DepartmentName = "HR" }
            );

            // Seed Course data (3 courses per department, 18 total)
            modelBuilder.Entity<Course>().HasData(
                // IT Department (DepartmentId = 1)
                new Course { CourseId = 1, CourseName = "Advanced SQL", Description = "Master SQL for data management", DepartmentId = 1, Duration = "2 weeks", CertificationName = "SQL Expert" },
                new Course { CourseId = 2, CourseName = "Cybersecurity Basics", Description = "Learn to secure systems", DepartmentId = 1, Duration = "3 weeks", CertificationName = "Cybersecurity Fundamentals" },
                new Course { CourseId = 3, CourseName = "Cloud Computing", Description = "Introduction to AWS and Azure", DepartmentId = 1, Duration = "4 weeks", CertificationName = "Cloud Practitioner" },

                // Finance Department (DepartmentId = 2)
                new Course { CourseId = 4, CourseName = "Financial Analysis", Description = "Analyze financial statements", DepartmentId = 2, Duration = "3 weeks", CertificationName = "Finance Analyst" },
                new Course { CourseId = 5, CourseName = "Risk Management", Description = "Manage financial risks", DepartmentId = 2, Duration = "2 weeks", CertificationName = "Risk Manager" },
                new Course { CourseId = 6, CourseName = "Accounting Principles", Description = "Basics of accounting", DepartmentId = 2, Duration = "4 weeks", CertificationName = "Accounting Basics" },

                // Human Resources Department (DepartmentId = 3)
                new Course { CourseId = 7, CourseName = "Recruitment Strategies", Description = "Effective hiring techniques", DepartmentId = 3, Duration = "2 weeks", CertificationName = "Recruitment Specialist" },
                new Course { CourseId = 8, CourseName = "Employee Engagement", Description = "Boost workplace morale", DepartmentId = 3, Duration = "3 weeks", CertificationName = "Engagement Expert" },
                new Course { CourseId = 9, CourseName = "Labor Law", Description = "Understand employment laws", DepartmentId = 3, Duration = "4 weeks", CertificationName = "Labor Law Certified" },

                // Marketing Department (DepartmentId = 4)
                new Course { CourseId = 10, CourseName = "Digital Marketing", Description = "Online marketing strategies", DepartmentId = 4, Duration = "3 weeks", CertificationName = "Digital Marketer" },
                new Course { CourseId = 11, CourseName = "Brand Management", Description = "Build strong brands", DepartmentId = 4, Duration = "2 weeks", CertificationName = "Brand Manager" },
                new Course { CourseId = 12, CourseName = "Market Research", Description = "Analyze market trends", DepartmentId = 4, Duration = "4 weeks", CertificationName = "Market Researcher" },

                // Operations Department (DepartmentId = 5)
                new Course { CourseId = 13, CourseName = "Process Optimization", Description = "Improve operational efficiency", DepartmentId = 5, Duration = "3 weeks", CertificationName = "Process Expert" },
                new Course { CourseId = 14, CourseName = "Supply Chain Management", Description = "Manage logistics", DepartmentId = 5, Duration = "4 weeks", CertificationName = "Supply Chain Specialist" },
                new Course { CourseId = 15, CourseName = "Project Management", Description = "Lead projects effectively", DepartmentId = 5, Duration = "2 weeks", CertificationName = "Project Manager" },

                // HR Department (DepartmentId = 6)
                new Course { CourseId = 16, CourseName = "HR Analytics", Description = "Use data in HR decisions", DepartmentId = 6, Duration = "3 weeks", CertificationName = "HR Analyst" },
                new Course { CourseId = 17, CourseName = "Conflict Resolution", Description = "Resolve workplace disputes", DepartmentId = 6, Duration = "2 weeks", CertificationName = "Conflict Mediator" },
                new Course { CourseId = 18, CourseName = "Performance Management", Description = "Evaluate employee performance", DepartmentId = 6, Duration = "4 weeks", CertificationName = "Performance Specialist" }
            );
        }
    }
}