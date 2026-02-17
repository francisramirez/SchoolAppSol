using SchoolAppSol.Domain.Base;
namespace SchoolAppSol.Domain.Entities
{
    public sealed class Department : AuditableEntity<int>
    {
        public string Name { get; private set; } = default!;
        public decimal Budget { get; private set; }
        public DateTime StartDate { get; private set; }
        public int? Administrator { get; private set; }
    }
}
