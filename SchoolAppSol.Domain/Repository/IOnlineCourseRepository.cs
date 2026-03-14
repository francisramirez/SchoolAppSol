using SchoolAppSol.Domain.Abstractions.Base;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Domain.Repository
{
    public interface IOnlineCourseRepository : IRepository<OnlineCourse, int>
    {
        Task<OnlineCourseModel?> GetByCourseIdAsync(int courseId, CancellationToken ct = default);
    }
}
