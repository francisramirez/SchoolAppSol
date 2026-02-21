


using SchoolAppSol.Domain.Base;

namespace SchoolAppSol.Domain.Entities;

public partial class OnlineCourse : Entity<int>
{
    public int CourseId { get; set; }

    public string? Url { get; set; }
}