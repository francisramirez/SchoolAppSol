namespace SchoolAppSol.Domain.Models
{
    public class CourseEnrollmentModel
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public int StudentId { get; set; }
        public string StudentFullName { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public int EnrollmentStatusId { get; set; }
        public string EnrollmentStatusName { get; set; } = string.Empty;
        public DateTime? ModifyDate { get; set; }
    }
}
