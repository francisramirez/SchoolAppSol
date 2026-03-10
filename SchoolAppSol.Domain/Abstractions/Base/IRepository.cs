using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Domain.Abstractions.Base
{
    public interface IRepository<TEntity, TId>
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(TId id, CancellationToken ct = default);
        Task AddAsync(TEntity entity, CancellationToken ct = default);
        Task UpdateAsync(TEntity entity, CancellationToken ct = default);
        Task SoftDeleteAsync(TId id, int userId, CancellationToken ct = default);
    }
}
