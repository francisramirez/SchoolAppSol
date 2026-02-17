namespace SchoolAppSol.Domain.Base
{
    public abstract class Entity<Tkey>
    {
        public Tkey Id { get; set; }
    }
}
