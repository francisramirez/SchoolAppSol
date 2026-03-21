namespace SchoolAppSol.Application.Dtos.CourseEnrollment
{
    public record CourseEnrollmentAddDto
    {
        public int CourseId { get; init; }
        public int StudentId { get; init; }
        public DateTime EnrollmentDate { get; init; }
        public int EnrollmentStatusId { get; init; }
        public int CreateUser { get; init; }
        public DateTime CreationDate { get; init; }
    }
}
