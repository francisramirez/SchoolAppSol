#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAppSol.Domain.Entities;

[Table("OnsiteCourse")]
public partial class OnsiteCourse : Base.Entity<int>
{
    [Key]
    public int CourseId { get; set; }

    public string Location { get; set; }

    public string Days { get; set; }

    public DateTime Time { get; set; }
}