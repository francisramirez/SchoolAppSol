

namespace SchoolAppSol.Domain.Base
{
    public interface ISoftDeleteTable
    {
        bool Deleted { get; }
        DateTime? DeletedDate { get; }
        int? UserDeleted { get; }
    }
}
