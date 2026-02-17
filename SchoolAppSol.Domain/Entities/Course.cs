using SchoolAppSol.Domain.Base;

namespace SchoolAppSol.Domain.Entities
{
    public sealed class Course : AuditableEntity<int>
    {
        public string Title { get; private set; } = default!;
        public int Credits { get; private set; }
        public int DepartmentID { get; private set; }
    }
}
