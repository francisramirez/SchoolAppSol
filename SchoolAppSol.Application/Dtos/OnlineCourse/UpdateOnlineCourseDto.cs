namespace SchoolAppSol.Application.Dtos.OnlineCourse
{
    public record UpdateOnlineCourseDto
    {
        public int Id { get; init; }
        public int CourseId { get; init; }
        public string Url { get; init; } = string.Empty;
    }
}
