using SchoolAppSol.Domain.Abstractions.Base;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Domain.Repository
{
    public interface IStudentRepository : IRepository<Student, int>
    {
        Task<IReadOnlyList<StudentModel>> GetAllActiveAsync(CancellationToken ct = default);
    }
}
