using SchoolAppSol.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAppSol.Domain.Entities
{
    public sealed class Course : Entity<int>
    {
        [Column("CourseID",TypeName ="int")]
        public override int Id { get; set; }
        public string Title { get; private set; } = default!;
        public int Credits { get; private set; }
        public int DepartmentID { get; private set; }
    }
}
