using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Common;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Validators.Interfaces;

namespace SchoolAppSol.Domain.Validators
{
    public sealed class CourseEnrollmentValidator : ICourseEnrollmentValidator
    {
        private readonly ICourseEnrollmentDomainRepository _enrollment;
        private readonly ICourseDomainRepository _course;
        private readonly IStudentDomainRepository _student;
        private readonly IEnrollmentStatusDomainRepository _enrollmentStatus;

        public CourseEnrollmentValidator(ICourseEnrollmentDomainRepository enrollmentDomainRepository,
                                         ICourseDomainRepository courseDomainRepository,
                                         IStudentDomainRepository studentDomainRepository,
                                         IEnrollmentStatusDomainRepository enrollmentStatusDomainRepository)
        {
            _enrollment = enrollmentDomainRepository;
            _course = courseDomainRepository;
            _student = studentDomainRepository;
            _enrollmentStatus = enrollmentStatusDomainRepository;
        }

        public async Task ValidateForCreateAsync(CourseEnrollment entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.GreaterThan(entity.CourseId, 0, nameof(entity.CourseId));
            Guard.GreaterThan(entity.StudentId, 0, nameof(entity.StudentId));
            Guard.GreaterThan(entity.EnrollmentStatusId, 0, nameof(entity.EnrollmentStatusId));
            Guard.GreaterThan(entity.CreationUser, 0, nameof(entity.CreationUser));
            Guard.NotFutureDate(entity.CreationDate, nameof(entity.CreationDate));
            Guard.NotFutureDate(entity.EnrollmentDate, nameof(entity.EnrollmentDate));

            if (!await _course.ExistsActiveAsync(entity.CourseId, ct))
                throw new DomainException("El curso no existe o está eliminado.");

            if (!await _student.ExistsActiveAsync(entity.StudentId, ct))
                throw new DomainException("El estudiante no existe o está eliminado.");

            if (!await _enrollmentStatus.ExistsActiveAsync(entity.EnrollmentStatusId, ct))
                throw new DomainException("El estado de inscripción no existe o está eliminado.");

            if (await _enrollment.ExistsActiveEnrollmentAsync(entity.CourseId, entity.StudentId, null, ct))
                throw new DomainException("El estudiante ya está inscrito en este curso.");
        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));

            if (!await _enrollment.ExistsActiveAsync(id, ct))
                throw new DomainException("La inscripción no existe o ya está eliminada.");
        }

        public async Task ValidateForUpdateAsync(CourseEnrollment entity, CancellationToken ct = default)
        {
            Guard.GreaterThan(entity.EnrollmentId, 0, nameof(entity.EnrollmentId));
            Guard.GreaterThan(entity.CourseId, 0, nameof(entity.CourseId));
            Guard.GreaterThan(entity.StudentId, 0, nameof(entity.StudentId));
            Guard.GreaterThan(entity.EnrollmentStatusId, 0, nameof(entity.EnrollmentStatusId));
            Guard.NotNull(entity.ModifyDate, nameof(entity.ModifyDate));
            Guard.GreaterThan(entity.UserMod ?? 0, 0, nameof(entity.UserMod));
            Guard.NotFutureDate(entity.EnrollmentDate, nameof(entity.EnrollmentDate));

            if (!await _enrollment.ExistsActiveAsync(entity.EnrollmentId, ct))
                throw new DomainException("La inscripción no existe o está eliminada.");

            if (!await _course.ExistsActiveAsync(entity.CourseId, ct))
                throw new DomainException("El curso no existe o está eliminado.");

            if (!await _student.ExistsActiveAsync(entity.StudentId, ct))
                throw new DomainException("El estudiante no existe o está eliminado.");

            if (!await _enrollmentStatus.ExistsActiveAsync(entity.EnrollmentStatusId, ct))
                throw new DomainException("El estado de inscripción no existe o está eliminado.");

            if (await _enrollment.ExistsActiveEnrollmentAsync(entity.CourseId, entity.StudentId, entity.EnrollmentId, ct))
                throw new DomainException("El estudiante ya tiene otra inscripción activa en este curso.");
        }
    }
}
