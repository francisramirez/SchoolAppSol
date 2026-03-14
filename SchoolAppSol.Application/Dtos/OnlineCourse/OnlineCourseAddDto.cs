namespace SchoolAppSol.Application.Dtos.OnlineCourse
{
    public record OnlineCourseAddDto
    {
        public int CourseId { get; init; }
        public string Url { get; init; } = string.Empty;
    }
}
