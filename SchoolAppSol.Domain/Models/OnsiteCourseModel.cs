
namespace SchoolAppSol.Domain.Models
{
    public class OnsiteCourseModel : CourseModel
    {
        public string Location { get; set; }
        public string Days { get; set; }
        public DateTime Time { get; set; }
    }
}
