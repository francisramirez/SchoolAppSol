using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolAppSol.Domain.Base;
namespace SchoolAppSol.Domain.Entities
{

    [Table("Departments")]
    public sealed class Department : Entity<int>
    {
        [Column("DepartmentID", TypeName = "int")]
        public override int Id { get; set; }
        public string Name { get; private set; } = default!;
        public decimal Budget { get; private set; }
        public DateTime StartDate { get; private set; }
        public int? Administrator { get; private set; }
    }
}
