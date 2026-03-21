

using Microsoft.EntityFrameworkCore;
using SchoolAppSol.Domain.Entities;

namespace SchoolAppSol.Persitence.Context
{
    public sealed class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EnrollmentStatus>  EnrollmentStatuses { get; set; }
        public DbSet<OnlineCourse> OnlineCourses { get; set; }
        public DbSet<OnsiteCourse> OnsiteCourses { get; set; }
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
