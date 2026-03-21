using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolAppSol.Application.Interfaces.Auth;
using SchoolAppSol.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolAppSol.Infrastructure.Services
{
    public sealed class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(User user, DateTime expiration)
        {
            // For production, the Key should be read from configuration and be at least 16 characters long.
            // Eg: var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "super_secret_key_that_needs_to_be_long");
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "a_very_long_secure_super_secret_key_here_for_jwt_signing");
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiration,
                SigningCredentials = credentials,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
