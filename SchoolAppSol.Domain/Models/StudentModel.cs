namespace SchoolAppSol.Domain.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? EnrollmentDate { get; set; }
    }
}
