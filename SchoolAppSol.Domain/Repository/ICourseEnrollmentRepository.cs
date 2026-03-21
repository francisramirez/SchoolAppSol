using SchoolAppSol.Domain.Abstractions.Base;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Domain.Repository
{
    public interface ICourseEnrollmentRepository : IRepository<CourseEnrollment, int>
    {
        Task<IReadOnlyList<CourseEnrollmentModel>> GetAllActiveAsync(CancellationToken ct = default);
        Task<IReadOnlyList<CourseEnrollmentModel>> GetByStudentIdAsync(int studentId, CancellationToken ct = default);
        Task<CourseEnrollmentModel?> GetByIdWithDetailsAsync(int id, CancellationToken ct = default);
    }
}
