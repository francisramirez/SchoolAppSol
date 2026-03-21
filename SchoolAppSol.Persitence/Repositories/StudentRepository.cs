using Microsoft.EntityFrameworkCore;
using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Persitence.Context;

namespace SchoolAppSol.Persitence.Repositories
{
    public sealed class StudentRepository : IStudentRepository, IStudentDomainRepository
    {
        private readonly SchoolContext _context;

        public StudentRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Student entity, CancellationToken ct = default)
        {
            await _context.Students.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> ExistsActiveAsync(int studentId, CancellationToken ct = default)
        {
            return await _context.Students.AsNoTracking()
                .AnyAsync(s => s.StudentId == studentId && !s.Deleted, ct);
        }

        public async Task<bool> ExistsActiveAsync(string firstName, string lastName, CancellationToken ct = default)
        {
            return await _context.Students.AsNoTracking()
                .AnyAsync(s => s.FirstName.Trim().ToLower() == firstName.Trim().ToLower() 
                            && s.LastName.Trim().ToLower() == lastName.Trim().ToLower() 
                            && !s.Deleted, ct);
        }

        public async Task<IReadOnlyList<StudentModel>> GetAllActiveAsync(CancellationToken ct = default)
        {
            return await _context.Students.AsNoTracking()
                .Where(s => !s.Deleted)
                .Select(s => new StudentModel
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    EnrollmentDate = s.EnrollmentDate
                })
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Students.AsNoTracking()
                .Where(s => !s.Deleted)
                .ToListAsync(ct);
        }

        public async Task<Student?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Students.AsNoTracking()
                .FirstOrDefaultAsync(s => s.StudentId == id && !s.Deleted, ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id, ct);
            if (student != null)
            {
                student.Deleted = true;
                student.DeletedDate = DateTime.UtcNow;
                student.UserDeleted = userId;
                _context.Students.Update(student);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task UpdateAsync(Student entity, CancellationToken ct = default)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == entity.StudentId, ct);
            
            if (student == null)
                throw new Persitence.Exceptions.PersistenceException("El estudiante no fue encontrado.");

            student.FirstName = entity.FirstName;
            student.LastName = entity.LastName;
            student.EnrollmentDate = entity.EnrollmentDate;
            student.ModifyDate = entity.ModifyDate;
            student.UserMod = entity.UserMod;

            _context.Students.Update(student);
            await _context.SaveChangesAsync(ct);
        }
    }
}
