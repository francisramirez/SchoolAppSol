using Microsoft.EntityFrameworkCore;
using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Persitence.Context;

namespace SchoolAppSol.Persitence.Repositories
{
    public sealed class CourseEnrollmentRepository : ICourseEnrollmentRepository, ICourseEnrollmentDomainRepository
    {
        private readonly SchoolContext _context;

        public CourseEnrollmentRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CourseEnrollment entity, CancellationToken ct = default)
        {
            await _context.CourseEnrollments.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> ExistsActiveAsync(int enrollmentId, CancellationToken ct = default)
        {
            return await _context.CourseEnrollments.AsNoTracking()
                .AnyAsync(ce => ce.EnrollmentId == enrollmentId && !ce.Deleted, ct);
        }

        public async Task<bool> ExistsActiveEnrollmentAsync(int courseId, int studentId, int? excludingEnrollmentId, CancellationToken ct = default)
        {
            return await _context.CourseEnrollments.AsNoTracking()
                .AnyAsync(ce => ce.CourseId == courseId 
                            && ce.StudentId == studentId 
                            && !ce.Deleted 
                            && (!excludingEnrollmentId.HasValue || ce.EnrollmentId != excludingEnrollmentId.Value), ct);
        }

        public async Task<IReadOnlyList<CourseEnrollmentModel>> GetAllActiveAsync(CancellationToken ct = default)
        {
            return await (from ce in _context.CourseEnrollments
                          join c in _context.Courses on ce.CourseId equals c.CourseId
                          join s in _context.Students on ce.StudentId equals s.StudentId
                          join es in _context.EnrollmentStatuses on ce.EnrollmentStatusId equals es.Id
                          where !ce.Deleted && !c.Deleted && !s.Deleted
                          select new CourseEnrollmentModel
                          {
                              EnrollmentId = ce.EnrollmentId,
                              CourseId = ce.CourseId,
                              CourseTitle = c.Title,
                              StudentId = ce.StudentId,
                              StudentFullName = s.FirstName + " " + s.LastName,
                              EnrollmentDate = ce.EnrollmentDate,
                              EnrollmentStatusId = ce.EnrollmentStatusId,
                              EnrollmentStatusName = es.Name,
                              ModifyDate = ce.ModifyDate
                          }).ToListAsync(ct);
        }

        public async Task<IReadOnlyList<CourseEnrollment>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.CourseEnrollments.AsNoTracking()
                .Where(ce => !ce.Deleted)
                .ToListAsync(ct);
        }

        public async Task<CourseEnrollment?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.CourseEnrollments.AsNoTracking()
                .FirstOrDefaultAsync(ce => ce.EnrollmentId == id && !ce.Deleted, ct);
        }

        public async Task<CourseEnrollmentModel?> GetByIdWithDetailsAsync(int id, CancellationToken ct = default)
        {
            return await (from ce in _context.CourseEnrollments
                          join c in _context.Courses on ce.CourseId equals c.CourseId
                          join s in _context.Students on ce.StudentId equals s.StudentId
                          join es in _context.EnrollmentStatuses on ce.EnrollmentStatusId equals es.Id
                          where ce.EnrollmentId == id && !ce.Deleted
                          select new CourseEnrollmentModel
                          {
                              EnrollmentId = ce.EnrollmentId,
                              CourseId = ce.CourseId,
                              CourseTitle = c.Title,
                              StudentId = ce.StudentId,
                              StudentFullName = s.FirstName + " " + s.LastName,
                              EnrollmentDate = ce.EnrollmentDate,
                              EnrollmentStatusId = ce.EnrollmentStatusId,
                              EnrollmentStatusName = es.Name,
                              ModifyDate = ce.ModifyDate
                          }).FirstOrDefaultAsync(ct);
        }

        public async Task<IReadOnlyList<CourseEnrollmentModel>> GetByStudentIdAsync(int studentId, CancellationToken ct = default)
        {
            return await (from ce in _context.CourseEnrollments
                          join c in _context.Courses on ce.CourseId equals c.CourseId
                          join s in _context.Students on ce.StudentId equals s.StudentId
                          join es in _context.EnrollmentStatuses on ce.EnrollmentStatusId equals es.Id
                          where ce.StudentId == studentId && !ce.Deleted && !c.Deleted
                          select new CourseEnrollmentModel
                          {
                              EnrollmentId = ce.EnrollmentId,
                              CourseId = ce.CourseId,
                              CourseTitle = c.Title,
                              StudentId = ce.StudentId,
                              StudentFullName = s.FirstName + " " + s.LastName,
                              EnrollmentDate = ce.EnrollmentDate,
                              EnrollmentStatusId = ce.EnrollmentStatusId,
                              EnrollmentStatusName = es.Name,
                              ModifyDate = ce.ModifyDate
                          }).ToListAsync(ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            var enrollment = await _context.CourseEnrollments.FirstOrDefaultAsync(ce => ce.EnrollmentId == id, ct);
            if (enrollment != null)
            {
                enrollment.Deleted = true;
                enrollment.DeletedDate = DateTime.UtcNow;
                enrollment.UserDeleted = userId;
                _context.CourseEnrollments.Update(enrollment);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task UpdateAsync(CourseEnrollment entity, CancellationToken ct = default)
        {
            var enrollment = await _context.CourseEnrollments.FirstOrDefaultAsync(ce => ce.EnrollmentId == entity.EnrollmentId, ct);
            
            if (enrollment == null)
                throw new Persitence.Exceptions.PersistenceException("La inscripción no fue encontrada.");

            enrollment.CourseId = entity.CourseId;
            enrollment.StudentId = entity.StudentId;
            enrollment.EnrollmentDate = entity.EnrollmentDate;
            enrollment.EnrollmentStatusId = entity.EnrollmentStatusId;
            enrollment.ModifyDate = entity.ModifyDate;
            enrollment.UserMod = entity.UserMod;

            _context.CourseEnrollments.Update(enrollment);
            await _context.SaveChangesAsync(ct);
        }
    }
}
