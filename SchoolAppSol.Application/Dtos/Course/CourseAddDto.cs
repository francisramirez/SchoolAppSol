

namespace SchoolAppSol.Application.Dtos.Course
{
    public record CourseAddDto
    {
        public string Title { get; init; } = string.Empty;
        public int Credits { get; init; }
        public int DepartmentId { get; init; }
        public int CreateUser { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
