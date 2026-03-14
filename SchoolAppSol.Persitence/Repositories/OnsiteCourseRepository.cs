using Microsoft.EntityFrameworkCore;
using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Persitence.Context;

namespace SchoolAppSol.Persitence.Repositories
{
    public sealed class OnsiteCourseRepository : IOnsiteCourseRepository, IOnsiteCourseDomainRepository
    {
        private readonly SchoolContext _context;

        public OnsiteCourseRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OnsiteCourse entity, CancellationToken ct = default)
        {
            await _context.OnsiteCourses.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> ExistsForCourseAsync(int courseId, int? excludingOnsiteCourseId, CancellationToken ct = default)
        {
            return await _context.OnsiteCourses.AsNoTracking()
                .AnyAsync(oc => oc.CourseId == courseId 
                            && (!excludingOnsiteCourseId.HasValue || oc.CourseId != excludingOnsiteCourseId.Value), ct);
        }

        public async Task<IReadOnlyList<OnsiteCourse>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.OnsiteCourses.AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<OnsiteCourseModel?> GetByCourseIdAsync(int courseId, CancellationToken ct = default)
        {
            return await (from oc in _context.OnsiteCourses
                          join c in _context.Courses on oc.CourseId equals c.CourseId
                          where oc.CourseId == courseId && !c.Deleted
                          select new OnsiteCourseModel
                          {
                              CourseId = oc.CourseId,
                              Location = oc.Location,
                              Days = oc.Days,
                              Time = oc.Time,
                              Title = c.Title,
                              Credits = c.Credits,
                              DepartmentId = c.DepartmentId
                          }).FirstOrDefaultAsync(ct);
        }

        public async Task<OnsiteCourse?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.OnsiteCourses.AsNoTracking()
                .FirstOrDefaultAsync(oc => oc.CourseId == id, ct);
        }

        public async Task<IReadOnlyList<OnsiteCourseModel>> SearchByLocationAsync(string term, CancellationToken ct = default)
        {
            return await (from oc in _context.OnsiteCourses
                          join c in _context.Courses on oc.CourseId equals c.CourseId
                          where oc.Location.Contains(term) && !c.Deleted
                          select new OnsiteCourseModel
                          {
                              CourseId = oc.CourseId,
                              Location = oc.Location,
                              Days = oc.Days,
                              Time = oc.Time,
                              Title = c.Title,
                              Credits = c.Credits,
                              DepartmentId = c.DepartmentId
                          })
                          .AsNoTracking()
                          .ToListAsync(ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            // OnsiteCourse doesn't have auditable fields or 'Deleted' in current schema based on entity inspection, 
            // so we implement an actual delete for now unless schema gets changed to BaseEntity
            var onsiteCourse = await _context.OnsiteCourses.FirstOrDefaultAsync(oc => oc.CourseId == id, ct);
            if (onsiteCourse != null)
            {
                _context.OnsiteCourses.Remove(onsiteCourse);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task UpdateAsync(OnsiteCourse entity, CancellationToken ct = default)
        {
            var onsiteCourse = await _context.OnsiteCourses.FirstOrDefaultAsync(oc => oc.CourseId == entity.CourseId, ct);
            
            if (onsiteCourse == null)
                throw new Persitence.Exceptions.PersistenceException("El curso presencial no fue encontrado.");

            onsiteCourse.CourseId = entity.CourseId;
            onsiteCourse.Location = entity.Location;
            onsiteCourse.Days = entity.Days;
            onsiteCourse.Time = entity.Time;

            _context.OnsiteCourses.Update(onsiteCourse);
            await _context.SaveChangesAsync(ct);
        }
    }
}
