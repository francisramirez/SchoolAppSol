

namespace SchoolAppSol.Application.Dtos.Course
{
    public record class RemoveCourseDto
    {
        public int Id { get; init; }
        public int DeleteUser { get; set; }
         public DateTime DeleteDate { get; set; }
    }
}
