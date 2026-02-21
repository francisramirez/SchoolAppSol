

namespace SchoolAppSol.Domain.Abstractions
{
    public interface IDepartmentDomainRepository
    {
        Task<bool> ExistsActiveAsync(int departmentId, CancellationToken ct = default);
        Task<bool> NameExistsAsync(string name, int? excludingDepartmentId, CancellationToken ct = default);

    }
}
