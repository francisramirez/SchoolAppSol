


using SchoolAppSol.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAppSol.Domain.Entities;

[Table("OnlineCourse")]
public partial class OnlineCourse : Entity<int>
{
    [Key]
    public int CourseId { get; set; }

    public string? Url { get; set; }
}