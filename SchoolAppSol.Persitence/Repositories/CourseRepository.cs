

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
             return await _context.Courses.FindAsync(new object[] { courseId }, ct) is Course course
                && !course.Deleted;
        }

        public Task<Course?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<CourseModel>> GetCoursesByDepartmentIdAsync(int departmentId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TitleExistsInDepartmentAsync(string title, int departmentId, int? excludingCourseId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Course entity, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
