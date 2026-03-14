namespace SchoolAppSol.Application.Dtos.OnsiteCourse
{
    public record UpdateOnsiteCourseDto
    {
        public int Id { get; init; }
        public int CourseId { get; init; }
        public string Location { get; init; } = string.Empty;
        public string Days { get; init; } = string.Empty;
        public DateTime Time { get; init; }
    }
}
