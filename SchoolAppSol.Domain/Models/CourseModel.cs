

namespace SchoolAppSol.Domain.Models
{
    public class CourseModel
    {
        public int CourseId { get; set; }

        public string? Title { get; set; }

        public int Credits { get; set; }

        public int DepartmentId { get; set; }
        public string? DepartmentDescription { get; set; }
    }
}
