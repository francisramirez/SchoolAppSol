
using SchoolAppSol.Domain.Base;

namespace SchoolAppSol.Domain.Entities;

public partial class Instructor : AuditableEntity<int>
{
    public int InstructorId { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public DateTime? HireDate { get; set; }

    
}