


namespace SchoolAppSol.Domain.Entities;

public partial class OfficeAssignment : Base.Entity<int>
{
    public int InstructorId { get; set; }

    public string? Location { get; set; }

    public byte[]? Timestamp { get; set; }
}