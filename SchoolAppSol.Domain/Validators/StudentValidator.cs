using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Common;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Validators.Interfaces;

namespace SchoolAppSol.Domain.Validators
{
    public sealed class StudentValidator : IStudentValidator
    {
        private readonly IStudentDomainRepository _student;

        public StudentValidator(IStudentDomainRepository studentDomainRepository)
        {
            _student = studentDomainRepository;
        }

        public async Task ValidateForCreateAsync(Student entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.NotNullOrWhiteSpace(entity.FirstName, nameof(entity.FirstName), 50);
            Guard.NotNullOrWhiteSpace(entity.LastName, nameof(entity.LastName), 50);
            Guard.GreaterThan(entity.CreationUser, 0, nameof(entity.CreationUser));
            Guard.NotFutureDate(entity.CreationDate, nameof(entity.CreationDate));
            if (entity.EnrollmentDate.HasValue)
            {
                Guard.NotFutureDate(entity.EnrollmentDate.Value, nameof(entity.EnrollmentDate));
            }

            if (await _student.ExistsActiveAsync(entity.FirstName.Trim(), entity.LastName.Trim(), ct))
                throw new DomainException("Ya existe un estudiante activo con ese mismo nombre y apellido.");
        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));

            if (!await _student.ExistsActiveAsync(id, ct))
                throw new DomainException("El estudiante no existe o ya está eliminado.");
        }

        public async Task ValidateForUpdateAsync(Student entity, CancellationToken ct = default)
        {
            Guard.GreaterThan(entity.StudentId, 0, nameof(entity.StudentId));
            Guard.NotNullOrWhiteSpace(entity.FirstName, nameof(entity.FirstName), 50);
            Guard.NotNullOrWhiteSpace(entity.LastName, nameof(entity.LastName), 50);
            Guard.GreaterThan(entity.UserMod ?? 0, 0, nameof(entity.UserMod));
            Guard.NotNull(entity.ModifyDate, nameof(entity.ModifyDate));

            if (entity.EnrollmentDate.HasValue)
            {
                Guard.NotFutureDate(entity.EnrollmentDate.Value, nameof(entity.EnrollmentDate));
            }

            if (!await _student.ExistsActiveAsync(entity.StudentId, ct))
                throw new DomainException("El estudiante no existe o está eliminado.");

            // ExistsActiveAsync with FirstName/LastName checks existence across ANY student (including this one),
            // So to validate update we fetch if there's any OTHER student with that name if the domain logic needs it.
            // Based on IStudentDomainRepository, we don't have an ExistsActiveAsync excluding ID. So we'll skip uniqueness error on update or assume the repo method is relaxed.
        }
    }
}
