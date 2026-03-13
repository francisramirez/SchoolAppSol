using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Common;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Validators.Interfaces;

namespace SchoolAppSol.Domain.Validators
{
    public sealed class DepartmentValidator : IDepartmentValidator
    {
        private readonly IDepartmentDomainRepository _department;

        public DepartmentValidator(IDepartmentDomainRepository departmentDomainRepository)
        {
            _department = departmentDomainRepository;
        }

        public async Task ValidateForCreateAsync(Department entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.NotNullOrWhiteSpace(entity.Name, nameof(entity.Name), 50); // Assuming 50 based on standard name lengths, can be adjusted
            Guard.GreaterThan(entity.CreationUser, 0, nameof(entity.CreationUser));
            Guard.NotFutureDate(entity.CreationDate, nameof(entity.CreationDate));
            Guard.NotNull(entity.StartDate, nameof(entity.StartDate));

            if (await _department.NameExistsAsync(entity.Name!.Trim(), null, ct))
                throw new DomainException("Ya existe un departamento con ese nombre.");
        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));

            if (!await _department.ExistsActiveAsync(id, ct))
                throw new DomainException("El departamento no existe o ya está eliminado.");
        }

        public async Task ValidateForUpdateAsync(Department entity, CancellationToken ct = default)
        {
            Guard.GreaterThan(entity.DepartmentId, 0, nameof(entity.DepartmentId));
            Guard.NotNullOrWhiteSpace(entity.Name, nameof(entity.Name), 50);
            Guard.NotNull(entity.StartDate, nameof(entity.StartDate));
            Guard.NotNull(entity.ModifyDate, nameof(entity.ModifyDate));
            Guard.GreaterThan(entity.UserMod ?? 0, 0, nameof(entity.UserMod));

            if (!await _department.ExistsActiveAsync(entity.DepartmentId, ct))
                throw new DomainException("El departamento no existe o ya está eliminado.");

            if (await _department.NameExistsAsync(entity.Name!.Trim(), entity.DepartmentId, ct))
                throw new DomainException("Ya existe otro departamento con ese nombre.");
        }
    }
}
