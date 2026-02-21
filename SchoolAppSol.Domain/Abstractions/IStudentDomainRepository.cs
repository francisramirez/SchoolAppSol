

namespace SchoolAppSol.Domain.Abstractions
{
    public interface IStudentDomainRepository
    {
        Task<bool> ExistsActiveAsync(int studentId, CancellationToken ct = default);
        Task<bool> ExistsActiveAsync(string firstName, string lastName, CancellationToken ct = default);
    }
}
