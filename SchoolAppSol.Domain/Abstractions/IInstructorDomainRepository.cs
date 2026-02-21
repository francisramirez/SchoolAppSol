
namespace SchoolAppSol.Domain.Abstractions
{
    public interface IInstructorDomainRepository
    {
        Task<bool> ExistsActiveAsync(int instructorId, CancellationToken ct = default);
        Task<bool> ExistsActiveAsync(string firstName, string lastName, CancellationToken ct = default);
    }
}
