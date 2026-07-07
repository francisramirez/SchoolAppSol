

using Microsoft.EntityFrameworkCore;
using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Persitence.Context;

namespace SchoolAppSol.Persitence.Repositories
{
    public sealed class DepartmentRepository : IDepartmentRepository, IDepartmentDomainRepository
    {
        private readonly SchoolContext _context;
        public DepartmentRepository(SchoolContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Department entity, CancellationToken ct = default)
        {
            await _context.Departments.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> ExistsActiveAsync(int departmentId, CancellationToken ct = default)
        {
            return await _context.Departments.AsNoTracking()
                .AnyAsync(d => d.DepartmentId == departmentId && !d.Deleted, ct);
        }

        /// <summary>
        /// Obtiene todos los departamentos activos (no eliminados) y los proyecta a DepartmentModel.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<DepartmentModel>> GetAllActiveAsync(CancellationToken ct = default)
        {
            return await _context.Departments
                .Where(cd => !cd.Deleted)
                .OrderByDescending(cd => cd.DepartmentId)
                .AsNoTracking()
                .Select(cd => new DepartmentModel()
                {
                    Budget = cd.Budget,
                    DepartmentId = cd.DepartmentId, 
                    Name = cd.Name, 
                    StartDate = cd.StartDate,
                }).ToListAsync();
        }

        public async Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken ct = default)
        {


            return await _context.Departments
                .Where(d => !d.Deleted)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<Department?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DepartmentId == id && !d.Deleted, ct);
        }

        public async Task<bool> NameExistsAsync(string name, int? excludingDepartmentId, CancellationToken ct = default)
        {
            return await _context.Departments
                .AsNoTracking()
                .AnyAsync(d => d.Name == name && (!excludingDepartmentId.HasValue || d.DepartmentId != excludingDepartmentId.Value) && !d.Deleted, ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == id && !d.Deleted, ct);

            if (department is null)
                throw new Persitence.Exceptions.PersistenceException("El departamento no se encuentra registrado.");

            department.DeletedDate = DateTime.UtcNow;
            department.UserDeleted = userId;
            department.Deleted = true;

            _context.Departments.Update(department);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Department entity, CancellationToken ct = default)
        {
            var department = await _context.Departments.AsNoTracking()
                .FirstOrDefaultAsync(d => d.DepartmentId == entity.DepartmentId && !d.Deleted, ct);

            if (department is null)
                throw new Persitence.Exceptions.PersistenceException("El departamento no se encuentra registrado.");

            department.Name = entity.Name;
            department.Budget = entity.Budget;
            department.StartDate = entity.StartDate;
            department.Administrator = entity.Administrator;
            department.ModifyDate = DateTime.UtcNow;
            department.UserMod = entity.UserMod;

            _context.Departments.Update(department);
            await _context.SaveChangesAsync(ct);
        }
    }
}
