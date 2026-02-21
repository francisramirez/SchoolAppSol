

namespace SchoolAppSol.Domain.Abstractions
{
    public interface IEnrollmentStatusDomainRepository
    {
        Task<bool> ExistsAsync(int enrollmentStatusId, CancellationToken ct = default);
        Task<bool> ExistsActiveAsync(int enrollmentStatusId, CancellationToken ct = default); 
        Task<bool> CodeExistsAsync(string code, int? excludingId, CancellationToken ct = default);

    }
}
