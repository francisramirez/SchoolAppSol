

namespace SchoolAppSol.Domain.Models
{
    public class DepartmentModel
    {
        public int DepartmentId { get; set; }
        public string? Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
    }
}
