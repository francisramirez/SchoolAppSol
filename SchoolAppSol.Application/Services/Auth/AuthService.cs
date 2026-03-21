using Microsoft.Extensions.Logging;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.Auth;
using SchoolAppSol.Application.Interfaces.Auth;
using SchoolAppSol.Domain.Common;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Domain.Validators.Interfaces;

namespace SchoolAppSol.Application.Services.Auth
{
    public sealed class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserValidator _userValidator;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository,
                           IUserValidator userValidator,
                           ITokenService tokenService,
                           ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<ServiceResult<TokenResponseDto>> LoginAsync(LoginDto loginDto)
        {
            var result = new ServiceResult<TokenResponseDto>();
            try
            {
                var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
                if (user == null)
                {
                    result.Message = "Usuario o contraseña inválida.";
                    result.Success = false;
                    return result;
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
                if (!isPasswordValid)
                {
                    result.Message = "Usuario o contraseña inválida.";
                    result.Success = false;
                    return result;
                }

                var expiration = DateTime.UtcNow.AddHours(2);
                var token = _tokenService.GenerateJwtToken(user, expiration);

                result.Success = true;
                result.Data = new TokenResponseDto
                {
                    Token = token,
                    Expiration = expiration
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login.");
                result.Success = false;
                result.Message = "Error interno durante autenticación.";
            }

            return result;
        }

        public async Task<ServiceResult<bool>> RegisterAsync(RegisterDto registerDto)
        {
            var result = new ServiceResult<bool>();
            try
            {
                var user = new User
                {
                    Username = registerDto.Username,
                    Email = registerDto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                    CreationDate = DateTime.UtcNow,
                    CreationUser = 1 // System
                };

                await _userValidator.ValidateForCreateAsync(user);
                await _userRepository.AddAsync(user);

                result.Success = true;
                result.Data = true;
                result.Message = "Usuario registrado correctamente.";
            }
            catch (DomainException dex)
            {
                result.Success = false;
                result.Message = dex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user.");
                result.Success = false;
                result.Message = "Ocurrió un error registrando el usuario.";
            }

            return result;
        }
    }
}
