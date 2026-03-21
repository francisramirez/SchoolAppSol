namespace SchoolAppSol.Application.Dtos.Student
{
    public record StudentAddDto
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public DateTime? EnrollmentDate { get; init; }
        public int CreateUser { get; init; }
        public DateTime CreationDate { get; init; }
    }
}
