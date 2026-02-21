


using SchoolAppSol.Domain.Base;

namespace SchoolAppSol.Domain.Entities;

public partial class Course : AuditableEntity<int>
{
    public int CourseId { get; set; }

    public string? Title { get; set; }

    public int Credits { get; set; }

    public int DepartmentId { get; set; }

    
}