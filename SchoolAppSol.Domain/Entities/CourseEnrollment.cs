
using SchoolAppSol.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace SchoolAppSol.Domain.Entities;

public partial class CourseEnrollment : AuditableEntity<int>
{
    [Key]
    public int EnrollmentId { get; set; }

    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public DateTime EnrollmentDate { get; set; }

    public int EnrollmentStatusId { get; set; }

   
}