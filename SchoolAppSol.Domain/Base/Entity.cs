using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAppSol.Domain.Base
{
    public abstract class Entity<Tkey>
    {
        [NotMapped]
        public Tkey? Id { get; set; }
    }
}
