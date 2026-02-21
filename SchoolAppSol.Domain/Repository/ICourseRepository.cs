using SchoolAppSol.Domain.Abstractions.Base;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Domain.Repository
{
    public interface ICourseRepository : IRepository<Course, int>
    {
        Task<IReadOnlyList<CourseModel>> GetCoursesByDepartmentIdAsync(int departmentId, CancellationToken ct = default);

    }
}
