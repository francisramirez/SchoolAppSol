using SchoolAppSol.Domain.Abstractions.Base;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Domain.Repository
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default);
        Task<IReadOnlyList<UserModel>> GetAllActiveAsync(CancellationToken ct = default);
    }
}
