
using SchoolAppSol.Domain.Abstractions.Base;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;
 
namespace SchoolAppSol.Domain.Repository
{
    public interface IDepartmentRepository : IRepository<Department, int>
    {
        Task<IReadOnlyList<DepartmentModel>> GetAllActiveAsync(CancellationToken ct = default);
    }
}

