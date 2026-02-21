

namespace SchoolAppSol.Domain.Abstractions
{
    public interface ICourseEnrollmentDomainRepository
    {
        Task<bool> ExistsActiveAsync(int enrollmentId, CancellationToken ct = default);
        Task<bool> ExistsActiveEnrollmentAsync(int courseId, int studentId, int? excludingEnrollmentId, CancellationToken ct = default);

    }
}
