namespace SchoolAppSol.Application.Dtos.Student
{
    public record UpdateStudentDto
    {
        public int StudentId { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public DateTime? EnrollmentDate { get; init; }
        public int UpdateUser { get; init; }
        public DateTime UpdateDate { get; init; }
    }
}
