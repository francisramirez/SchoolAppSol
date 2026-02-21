namespace SchoolAppSol.Domain.Validators.Interfaces
{
    public interface IEntityValidator<in TEntity, in TId>
    {
        Task ValidateForCreateAsync(TEntity entity, CancellationToken ct = default);
        Task ValidateForUpdateAsync(TEntity entity, CancellationToken ct = default);
        Task ValidateForDeleteAsync(TId id, int userId, CancellationToken ct = default);
    }
}
