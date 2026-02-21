
#nullable disable



using SchoolAppSol.Domain.Base;

namespace SchoolAppSol.Domain.Entities;

public partial class StudentGrade : AuditableEntity<int>
{
    public int EnrollmentId { get; set; }

    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public decimal? Grade { get; set; }
}