


using SchoolAppSol.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAppSol.Domain.Entities;

[Table("Course")]
public partial class Course : AuditableEntity<int>
{
  
    private string? _title;
    public int CourseId { get; set; }

    public string? Title 
    { 
        get; 
        set; 
    }

    public int Credits { get; set; }

    public int DepartmentId { get; set; }

    
}