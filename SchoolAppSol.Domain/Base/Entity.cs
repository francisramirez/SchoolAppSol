namespace SchoolAppSol.Domain.Base
{
    public abstract class Entity<Tkey>
    {
        public abstract Tkey Id { get; set; }
    }
}
