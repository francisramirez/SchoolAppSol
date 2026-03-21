using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Common;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Validators.Interfaces;

namespace SchoolAppSol.Domain.Validators
{
    public sealed class UserValidator : IUserValidator
    {
        private readonly IUserDomainRepository _userDomainRepository;

        public UserValidator(IUserDomainRepository userDomainRepository)
        {
            _userDomainRepository = userDomainRepository;
        }

        public async Task ValidateForCreateAsync(User entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.NotNullOrWhiteSpace(entity.Username, nameof(entity.Username), 50);
            Guard.NotNullOrWhiteSpace(entity.Email, nameof(entity.Email), 100);
            Guard.NotNullOrWhiteSpace(entity.PasswordHash, nameof(entity.PasswordHash));
            Guard.GreaterThan(entity.CreationUser, 0, nameof(entity.CreationUser));
            Guard.NotFutureDate(entity.CreationDate, nameof(entity.CreationDate));

            if (!entity.Email.Contains('@'))
                throw new DomainException("El correo electrónico no es válido.");

            if (await _userDomainRepository.UsernameExistsAsync(entity.Username.Trim(), ct))
                throw new DomainException("Ya existe un usuario con este mismo nombre de usuario.");

            if (await _userDomainRepository.EmailExistsAsync(entity.Email.Trim(), ct))
                throw new DomainException("Ya existe un usuario registrado con este correo electrónico.");
        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));

            if (!await _userDomainRepository.ExistsActiveAsync(id, ct))
                throw new DomainException("El usuario no existe o ya está eliminado.");
        }

        public async Task ValidateForUpdateAsync(User entity, CancellationToken ct = default)
        {
            Guard.GreaterThan(entity.UserId, 0, nameof(entity.UserId));
            Guard.NotNullOrWhiteSpace(entity.Username, nameof(entity.Username), 50);
            Guard.NotNullOrWhiteSpace(entity.Email, nameof(entity.Email), 100);
            Guard.GreaterThan(entity.UserMod ?? 0, 0, nameof(entity.UserMod));
            Guard.NotNull(entity.ModifyDate, nameof(entity.ModifyDate));

            if (!entity.Email.Contains('@'))
                throw new DomainException("El correo electrónico no es válido.");

            if (!await _userDomainRepository.ExistsActiveAsync(entity.UserId, ct))
                throw new DomainException("El usuario no existe o está eliminado.");
        }
    }
}
