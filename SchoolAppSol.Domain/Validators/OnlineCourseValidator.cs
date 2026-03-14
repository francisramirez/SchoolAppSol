using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Common;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Validators.Interfaces;

namespace SchoolAppSol.Domain.Validators
{
    public sealed class OnlineCourseValidator : IOnlineCourseValidator
    {
        private readonly IOnlineCourseDomainRepository _onlineCourse;
        private readonly ICourseDomainRepository _course;

        public OnlineCourseValidator(IOnlineCourseDomainRepository onlineCourseDomainRepository,
                                     ICourseDomainRepository courseDomainRepository)
        {
            _onlineCourse = onlineCourseDomainRepository;
            _course = courseDomainRepository;
        }

        public async Task ValidateForCreateAsync(OnlineCourse entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.GreaterThan(entity.CourseId, 0, nameof(entity.CourseId));
            Guard.NotNullOrWhiteSpace(entity.Url, nameof(entity.Url), 500);

            if (!await _course.ExistsActiveAsync(entity.CourseId, ct))
                throw new DomainException("El curso base no existe o está eliminado.");

            if (await _onlineCourse.ExistsForCourseAsync(entity.CourseId, null, ct))
                throw new DomainException("Ya existe una configuración en línea para este curso.");
        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            // Note: Currently id conceptually represents CourseId based on repository implementations
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));
        }

        public async Task ValidateForUpdateAsync(OnlineCourse entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.GreaterThan(entity.CourseId, 0, nameof(entity.CourseId));
            Guard.NotNullOrWhiteSpace(entity.Url, nameof(entity.Url), 500);

            if (!await _course.ExistsActiveAsync(entity.CourseId, ct))
                throw new DomainException("El curso base no existe o está eliminado.");

            if (await _onlineCourse.ExistsForCourseAsync(entity.CourseId, entity.CourseId, ct))
                throw new DomainException("Ya existe otra configuración en línea para este curso.");
        }
    }
}
