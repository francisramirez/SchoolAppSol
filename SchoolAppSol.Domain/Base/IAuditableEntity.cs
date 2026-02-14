

namespace SchoolAppSol.Domain.Base
{
    public interface IAuditableEntity
    {
        DateTime CreationDate { get; }
        DateTime? ModifyDate { get; }
        int CreationUser { get; }
        int? UserMod { get; }
    }
}
