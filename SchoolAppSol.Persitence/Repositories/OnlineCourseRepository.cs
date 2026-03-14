using Microsoft.EntityFrameworkCore;
using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Persitence.Context;

namespace SchoolAppSol.Persitence.Repositories
{
    public sealed class OnlineCourseRepository : IOnlineCourseRepository, IOnlineCourseDomainRepository
    {
        private readonly SchoolContext _context;

        public OnlineCourseRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OnlineCourse entity, CancellationToken ct = default)
        {
            await _context.OnlineCourses.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> ExistsForCourseAsync(int courseId, int? excludingOnlineCourseId, CancellationToken ct = default)
        {
            return await _context.OnlineCourses.AsNoTracking()
                .AnyAsync(oc => oc.CourseId == courseId 
                            && (!excludingOnlineCourseId.HasValue || oc.CourseId != excludingOnlineCourseId.Value), ct);
        }

        public async Task<IReadOnlyList<OnlineCourse>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.OnlineCourses.AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<OnlineCourseModel?> GetByCourseIdAsync(int courseId, CancellationToken ct = default)
        {
            return await (from oc in _context.OnlineCourses
                          join c in _context.Courses on oc.CourseId equals c.CourseId
                          where oc.CourseId == courseId && !c.Deleted
                          select new OnlineCourseModel
                          {
                              CourseId = oc.CourseId,
                              Url = oc.Url,
                              Title = c.Title,
                              Credits = c.Credits,
                              DepartmentId = c.DepartmentId
                          }).FirstOrDefaultAsync(ct);
        }

        public async Task<OnlineCourse?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.OnlineCourses.AsNoTracking()
                .FirstOrDefaultAsync(oc => oc.CourseId == id, ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            var onlineCourse = await _context.OnlineCourses.FirstOrDefaultAsync(oc => oc.CourseId == id, ct);
            if (onlineCourse != null)
            {
                _context.OnlineCourses.Remove(onlineCourse);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task UpdateAsync(OnlineCourse entity, CancellationToken ct = default)
        {
            var onlineCourse = await _context.OnlineCourses.FirstOrDefaultAsync(oc => oc.CourseId == entity.CourseId, ct);
            
            if (onlineCourse == null)
                throw new Persitence.Exceptions.PersistenceException("El curso en línea no fue encontrado.");

            onlineCourse.CourseId = entity.CourseId;
            onlineCourse.Url = entity.Url;

            _context.OnlineCourses.Update(onlineCourse);
            await _context.SaveChangesAsync(ct);
        }
    }
}
