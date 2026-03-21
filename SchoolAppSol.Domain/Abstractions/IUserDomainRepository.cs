namespace SchoolAppSol.Domain.Abstractions
{
    public interface IUserDomainRepository
    {
        Task<bool> ExistsActiveAsync(int userId, CancellationToken ct = default);
        Task<bool> UsernameExistsAsync(string username, CancellationToken ct = default);
        Task<bool> EmailExistsAsync(string email, CancellationToken ct = default);
    }
}
