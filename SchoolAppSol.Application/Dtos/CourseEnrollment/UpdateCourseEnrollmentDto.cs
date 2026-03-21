namespace SchoolAppSol.Application.Dtos.CourseEnrollment
{
    public record UpdateCourseEnrollmentDto
    {
        public int EnrollmentId { get; init; }
        public int CourseId { get; init; }
        public int StudentId { get; init; }
        public DateTime EnrollmentDate { get; init; }
        public int EnrollmentStatusId { get; init; }
        public int UpdateUser { get; init; }
        public DateTime UpdateDate { get; init; }
    }
}
