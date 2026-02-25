

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
        public DbSet<EnrollmentStatus>  EnrollmentStatuses { get; set; }
    }
}
