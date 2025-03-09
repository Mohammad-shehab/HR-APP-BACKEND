using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HR_APP_BACKEND.Migrations
{
    /// <inheritdoc />
    public partial class ghjkl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CertificationName", "CourseName", "DepartmentId", "Description", "Duration" },
                values: new object[,]
                {
                    { 1, "SQL Expert", "Advanced SQL", 1, "Master SQL for data management", "2 weeks" },
                    { 2, "Cybersecurity Fundamentals", "Cybersecurity Basics", 1, "Learn to secure systems", "3 weeks" },
                    { 3, "Cloud Practitioner", "Cloud Computing", 1, "Introduction to AWS and Azure", "4 weeks" },
                    { 4, "Finance Analyst", "Financial Analysis", 2, "Analyze financial statements", "3 weeks" },
                    { 5, "Risk Manager", "Risk Management", 2, "Manage financial risks", "2 weeks" },
                    { 6, "Accounting Basics", "Accounting Principles", 2, "Basics of accounting", "4 weeks" },
                    { 7, "Recruitment Specialist", "Recruitment Strategies", 3, "Effective hiring techniques", "2 weeks" },
                    { 8, "Engagement Expert", "Employee Engagement", 3, "Boost workplace morale", "3 weeks" },
                    { 9, "Labor Law Certified", "Labor Law", 3, "Understand employment laws", "4 weeks" },
                    { 10, "Digital Marketer", "Digital Marketing", 4, "Online marketing strategies", "3 weeks" },
                    { 11, "Brand Manager", "Brand Management", 4, "Build strong brands", "2 weeks" },
                    { 12, "Market Researcher", "Market Research", 4, "Analyze market trends", "4 weeks" },
                    { 13, "Process Expert", "Process Optimization", 5, "Improve operational efficiency", "3 weeks" },
                    { 14, "Supply Chain Specialist", "Supply Chain Management", 5, "Manage logistics", "4 weeks" },
                    { 15, "Project Manager", "Project Management", 5, "Lead projects effectively", "2 weeks" },
                    { 16, "HR Analyst", "HR Analytics", 6, "Use data in HR decisions", "3 weeks" },
                    { 17, "Conflict Mediator", "Conflict Resolution", 6, "Resolve workplace disputes", "2 weeks" },
                    { 18, "Performance Specialist", "Performance Management", 6, "Evaluate employee performance", "4 weeks" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 18);
        }
    }
}
