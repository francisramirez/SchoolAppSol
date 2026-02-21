

using SchoolAppSol.Domain.Base;

namespace SchoolAppSol.Domain.Entities;

public partial class Department : AuditableEntity<int>
{
    public int DepartmentId { get; set; }

    public string? Name { get; set; }

    public decimal Budget { get; set; }

    public DateTime StartDate { get; set; }

    public int? Administrator { get; set; }

   
}