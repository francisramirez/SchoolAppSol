using SchoolAppSol.Domain.Entities;

namespace SchoolAppSol.Application.Interfaces.Auth
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user, DateTime expiration);
    }
}
