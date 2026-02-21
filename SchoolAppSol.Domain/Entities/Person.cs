#nullable disable



namespace SchoolAppSol.Domain.Entities;

public partial class Person : Base.Entity<int>
{
    public int PersonId { get; set; }

    public string LastName { get; set; }

    public string FirstName { get; set; }

    public DateTime? HireDate { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public string Discriminator { get; set; }
}