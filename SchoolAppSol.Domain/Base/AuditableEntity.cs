


namespace SchoolAppSol.Domain.Base
{
    public abstract class AuditableEntity<Tkey> : Entity<Tkey>  
    {
        public DateTime CreationDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int CreationUser { get; set; }

        public int? UserMod { get; set; }

        public int? UserDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool Deleted { get; set; }

        
    }
}
