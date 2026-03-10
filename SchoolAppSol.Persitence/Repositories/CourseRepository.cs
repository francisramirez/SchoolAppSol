using Microsoft.EntityFrameworkCore;
using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Persitence.Context;

namespace SchoolAppSol.Persitence.Repositories
{
    public sealed class CourseRepository : ICourseRepository, ICourseDomainRepository
    {
        private readonly SchoolContext _context;

        public CourseRepository(SchoolContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Course entity, CancellationToken ct = default)
        {
            await _context.Courses.AddAsync(entity, ct);
        }

        public async Task<bool> ExistsActiveAsync(int courseId, CancellationToken ct = default)
        {
            return await _context.Courses.AsNoTracking()
                .AnyAsync(c => c.Id == courseId
                          && !c.Deleted, ct);
        }

        public async Task<IReadOnlyList<Course>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Courses.Where(cd => !cd.Deleted)
                                         .AsNoTracking()
                                         .ToListAsync(ct);
        }
        public async Task<Course?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Courses
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(cd => cd.CourseId == id
                                                     && !cd.Deleted, ct);
        }

        public async Task<IReadOnlyList<CourseModel>> GetCoursesAsync(CancellationToken ct = default)
        {
            return await (from c in _context.Courses
                          join de in _context.Departments on c.DepartmentId equals de.DepartmentId
                          where c.Deleted == false
                          select new CourseModel
                          {
                              CourseId = c.CourseId,
                              DepartmentId = c.DepartmentId,
                              Credits = c.Credits,
                              DepartmentDescription = de.Name,
                              Title = c.Title
                          })
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<CourseModel>> GetCoursesByDepartmentIdAsync(int departmentId, CancellationToken ct = default)
        {
            return await (from c in _context.Courses
                          join de in _context.Departments on c.DepartmentId equals de.DepartmentId
                          where c.DepartmentId == departmentId
                          select new CourseModel
                          {
                              CourseId = c.CourseId,
                              DepartmentId = c.DepartmentId,
                              Credits = c.Credits,
                              DepartmentDescription = de.Name,
                              Title = c.Title
                          })
                 .AsNoTracking()
                 .ToListAsync(ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == id
                                                                    && !c.Deleted, ct);

            if (course is null)
                throw new Persitence.Exceptions.PersistenceException("El curso no se encuentra registrado.");

            course.DeletedDate = DateTime.UtcNow;
            course.UserDeleted = userId;
            course.Deleted = true;

            _context.Courses.Update(course);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> TitleExistsInDepartmentAsync(string title, int departmentId, int? excludingCourseId, CancellationToken ct = default)
        {
            return await _context.Courses
                .AsNoTracking()
                .AnyAsync(co => co.Title == title
                           && co.DepartmentId == departmentId, ct);
        }

        public async Task UpdateAsync(Course entity, CancellationToken ct = default)
        {

            var course = await _context.Courses.AsNoTracking()
                                               .FirstOrDefaultAsync(c => c.CourseId == entity.CourseId
                                                                   && !c.Deleted, ct);

            if (course is null)
                throw new Persitence.Exceptions.PersistenceException("El curso no se encuentra registrado.");


            course.Title = entity.Title;
            course.DepartmentId = entity.DepartmentId;
            course.ModifyDate = DateTime.UtcNow;
            course.UserMod = entity.UserMod;
            course.Credits = entity.Credits;

            _context.Courses.Update(course);

            await _context.SaveChangesAsync(ct);

        }
    }
}
