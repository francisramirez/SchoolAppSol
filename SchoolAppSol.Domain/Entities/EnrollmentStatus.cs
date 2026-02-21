


using SchoolAppSol.Domain.Base;

namespace SchoolAppSol.Domain.Entities;

public partial class EnrollmentStatus : AuditableEntity<short>
{
    public int EnrollmentStatusId { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    
}