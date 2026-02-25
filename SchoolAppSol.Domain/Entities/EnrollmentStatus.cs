


using SchoolAppSol.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAppSol.Domain.Entities;

[Table("Enrollment_Status")]
public partial class EnrollmentStatus : AuditableEntity<short>
{
    public int EnrollmentStatusId { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    
}