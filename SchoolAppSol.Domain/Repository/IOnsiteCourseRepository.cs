using SchoolAppSol.Domain.Abstractions.Base;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Domain.Repository
{
    public interface IOnsiteCourseRepository : IRepository<OnsiteCourse,int>
    {
        Task<OnsiteCourseModel?> GetByCourseIdAsync(int courseId, CancellationToken ct = default);
        Task<IReadOnlyList<OnsiteCourseModel>> SearchByLocationAsync(string term, CancellationToken ct = default);

    }
}
