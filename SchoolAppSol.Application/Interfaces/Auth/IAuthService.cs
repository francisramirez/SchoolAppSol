using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.Auth;

namespace SchoolAppSol.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<ServiceResult<TokenResponseDto>> LoginAsync(LoginDto loginDto);
        Task<ServiceResult<bool>> RegisterAsync(RegisterDto registerDto);
    }
}
