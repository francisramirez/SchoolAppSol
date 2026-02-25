

using Microsoft.EntityFrameworkCore;
using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Persitence.Context;
using SchoolAppSol.Persitence.Exceptions;

namespace SchoolAppSol.Persitence.Repositories
{
    public sealed class CourseRepository : ICourseRepository, ICourseDomainRepository
    {
        private readonly SchoolContext context;
        public CourseRepository(SchoolContext context)
        {
            this.context = context;
        }

        #region "Repository"
        public async Task AddAsync(Course entity, CancellationToken ct = default)
        {
            Course course = new Course()
            {
                Title = entity.Title,
                Credits = entity.Credits,
                DepartmentId = entity.DepartmentId
            };

            context.Courses.Add(course);
            return await context.SaveChangesAsync(ct);

        }

        public Task<IReadOnlyList<CourseModel>> GetCoursesByDepartmentIdAsync(int departmentId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            Course course = await context.Courses.FindAsync(new object[] { id }, ct);

            //if (course == null)
            //    throw new PersistenceException("El curso que desea eliminar no se encuentra registrado.");

            course.IsDeleted = true;
            course.DeletedBy = userId;
            course.DeletedAt = DateTime.UtcNow;
            context.Courses.Update(course);
            await context.SaveChangesAsync(ct);

        }

        public async Task<bool> TitleExistsInDepartmentAsync(string title, int departmentId, int? excludingCourseId, CancellationToken ct = default)
        {
            return await context.Courses.AnyAsync(c => c.Title == title 
                            && c.DepartmentId == departmentId 
                            && c.Id != excludingCourseId, ct);
        }

        public async Task UpdateAsync(Course entity, CancellationToken ct = default)
        {
            Course course = await context.Courses.FindAsync(new object[] { id }, ct);

            course.Title = entity.Title;
            course.Credits = entity.Credits;
            course.DepartmentId = entity.DepartmentId;
            course.ModifyDate = DateTime.UtcNow;
            course.UserMod= entity.UserMod;


            await context.SaveChangesAsync(ct);
        }
        #endregion

        #region"Domian Services Methods"

        public async Task<bool> ExistsActiveAsync(int courseId, CancellationToken ct = default)
        {
            return await context.Courses.AnyAsync(c => c.Id = courseId, 
                                                   ct);
        }

        public async Task<Course?> GetByIdAsync(int id, CancellationToken ct = default)
        {
             return await context.Courses.FindAsync(new object[] { id }, ct);
        }
        #endregion
    }
}
