using SchoolAppSol.Domain.Entities;

namespace SchoolAppSol.Domain.Validators.Interfaces
{
    public interface ICourseValidator : IEntityValidator<Course,int>
    {
        // Additional validation methods specific to Course can be added here
    }
}
