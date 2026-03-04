

namespace SchoolAppSol.Application.Dtos.Course
{
    public record UpdateCourseDto
    {
        public int Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public int Credits { get; init; }
        public int DepartmentId { get; init; }
        public int UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
