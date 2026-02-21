
using SchoolAppSol.Domain.Base;

namespace SchoolAppSol.Domain.Entities;

public partial class CourseEnrollment : AuditableEntity<int>
{
    public int EnrollmentId { get; set; }

    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public DateTime EnrollmentDate { get; set; }

    public int EnrollmentStatusId { get; set; }

   
}