#nullable disable

namespace SchoolAppSol.Domain.Entities;

public partial class OnsiteCourse : Base.Entity<int>
{
    public int CourseId { get; set; }

    public string Location { get; set; }

    public string Days { get; set; }

    public DateTime Time { get; set; }
}