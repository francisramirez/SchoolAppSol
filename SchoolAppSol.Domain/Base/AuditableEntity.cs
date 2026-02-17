


namespace SchoolAppSol.Domain.Base
{
    public abstract class AuditableEntity<Tkey> : Entity<Tkey>  
    {
        public DateTime CreationDate { get; protected set; } = DateTime.UtcNow;
        public DateTime? ModifyDate { get; protected set; }
        public int CreationUser { get; protected set; } = 1;
        public int? UserMod { get; protected set; }

        public bool Deleted { get; protected set; }
        public DateTime? DeletedDate { get; protected set; }
        public int? UserDeleted { get; protected set; }

        public void MarkModified(int userId)
        {
            ModifyDate = DateTime.UtcNow;
            UserMod = userId;
        }

        public void SoftDelete(int userId)
        {
            Deleted = true;
            DeletedDate = DateTime.UtcNow;
            UserDeleted = userId;
        }
    }
}
