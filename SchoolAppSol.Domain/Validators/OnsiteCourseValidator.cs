using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Common;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Validators.Interfaces;

namespace SchoolAppSol.Domain.Validators
{
    public sealed class OnsiteCourseValidator : IOnsiteCourseValidator
    {
        private readonly IOnsiteCourseDomainRepository _onsiteCourse;
        private readonly ICourseDomainRepository _course;

        public OnsiteCourseValidator(IOnsiteCourseDomainRepository onsiteCourseDomainRepository,
                                     ICourseDomainRepository courseDomainRepository)
        {
            _onsiteCourse = onsiteCourseDomainRepository;
            _course = courseDomainRepository;
        }

        public async Task ValidateForCreateAsync(OnsiteCourse entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.GreaterThan(entity.CourseId, 0, nameof(entity.CourseId));
            Guard.NotNullOrWhiteSpace(entity.Location, nameof(entity.Location), 150);
            Guard.NotNullOrWhiteSpace(entity.Days, nameof(entity.Days), 100);

            if (!await _course.ExistsActiveAsync(entity.CourseId, ct))
                throw new DomainException("El curso base no existe o está eliminado.");

            if (await _onsiteCourse.ExistsForCourseAsync(entity.CourseId, null, ct))
                throw new DomainException("Ya existe una configuración presencial para este curso.");
        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            // Note: id conceptually represents CourseId based on repository implementations
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));
        }

        public async Task ValidateForUpdateAsync(OnsiteCourse entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.GreaterThan(entity.CourseId, 0, nameof(entity.CourseId));
            Guard.NotNullOrWhiteSpace(entity.Location, nameof(entity.Location), 150);
            Guard.NotNullOrWhiteSpace(entity.Days, nameof(entity.Days), 100);

            if (!await _course.ExistsActiveAsync(entity.CourseId, ct))
                throw new DomainException("El curso base no existe o está eliminado.");

            if (await _onsiteCourse.ExistsForCourseAsync(entity.CourseId, entity.CourseId, ct))
                throw new DomainException("Ya existe otra configuración presencial para este curso.");
        }
    }
}
