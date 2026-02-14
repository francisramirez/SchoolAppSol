


namespace SchoolAppSol.Domain.Base
{
    public abstract class AuditableEntity : IAuditableEntity, ISoftDeleteTable
    {
        public DateTime CreationDate => throw new NotImplementedException();

        public DateTime? ModifyDate => throw new NotImplementedException();

        public int CreationUser => throw new NotImplementedException();

        public int? UserMod => throw new NotImplementedException();

        public bool Deleted => throw new NotImplementedException();

        public DateTime? DeletedDate => throw new NotImplementedException();

        public int? UserDeleted => throw new NotImplementedException();
    }
}
