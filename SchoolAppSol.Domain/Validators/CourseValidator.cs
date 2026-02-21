

using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Common;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Validators.Interfaces;

namespace SchoolAppSol.Domain.Validators
{
    public sealed class CourseValidator : ICourseValidator
    {
        private readonly ICourseDomainRepository _course;
        private readonly IDepartmentDomainRepository _department;

        public CourseValidator(ICourseDomainRepository courseDomainRepository,
                               IDepartmentDomainRepository departmentDomainRepository)
        {
            _course = courseDomainRepository;
            _department = departmentDomainRepository;
        }
        public async Task ValidateForCreateAsync(Course entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.NotNullOrWhiteSpace(entity.Title, nameof(entity.Title), 200);
            Guard.GreaterThan(entity.Credits, 0, nameof(entity.Credits));
            Guard.GreaterThan(entity.DepartmentId, 0, nameof(entity.DepartmentId));
            Guard.GreaterThan(entity.CreationUser, 0, nameof(entity.CreationUser));
            Guard.NotFutureDate(entity.CreationDate, nameof(entity.CreationDate));

            if (!await _department.ExistsActiveAsync(entity.DepartmentId, ct))
                throw new DomainException("DepartmentId no existe o está eliminado.");

            if (await _course.TitleExistsInDepartmentAsync(entity.Title.Trim(), entity.DepartmentId, null, ct))
                throw new DomainException("Ya existe un curso con ese título en el departamento.");

        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));

            if (!await _course.ExistsActiveAsync(id, ct))
                throw new DomainException("El curso no existe o ya está eliminado.");
        }

        public async Task ValidateForUpdateAsync(Course entity, CancellationToken ct = default)
        {
            Guard.GreaterThan(entity.CourseId, 0, nameof(entity.CourseId));
            Guard.NotNullOrWhiteSpace(entity.Title, nameof(entity.Title), 200);
            Guard.GreaterThan(entity.Credits, 0, nameof(entity.Credits));
            Guard.GreaterThan(entity.DepartmentId, 0, nameof(entity.DepartmentId));
            Guard.NotNull(entity.ModifyDate, nameof(entity.ModifyDate));
            Guard.GreaterThan(entity.UserMod ?? 0, 0, nameof(entity.UserMod));

            if (!await _course.ExistsActiveAsync(entity.CourseId, ct))
                throw new DomainException("El curso no existe o está eliminado.");

            if (!await _department.ExistsActiveAsync(entity.DepartmentId, ct))
                throw new DomainException("DepartmentId no existe o está eliminado.");

            if (await _course.TitleExistsInDepartmentAsync(entity.Title.Trim(), entity.DepartmentId, entity.CourseId, ct))
                throw new DomainException("Ya existe otro curso con ese título en el departamento.");
        }
    }
}
