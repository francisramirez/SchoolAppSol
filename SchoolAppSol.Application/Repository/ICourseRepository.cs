
using SchoolAppSol.Domain.Entities;

namespace SchoolAppSol.Application.Repository
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        // Additional methods specific to Course can be added here
       Task<IEnumerable<Course>> GetCoursesByDepartmentIdAsync(int departmentId);
    }
}
