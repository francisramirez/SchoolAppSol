

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
        public Task AddAsync(Department entity, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsActiveAsync(int departmentId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<DepartmentModel>> GetAllActiveAsync(CancellationToken ct = default)
        {
            return await _context.Departments
                .Where(cd => !cd.Deleted)
                .AsNoTracking()
                .Select(cd => new DepartmentModel()
                {
                    Budget = cd.Budget,
                    DepartmentId = cd.DepartmentId, 
                    Name = cd.Name, 
                    StartDate = cd.StartDate,
                }).ToListAsync();
        }

        public Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Department?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> NameExistsAsync(string name, int? excludingDepartmentId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Department entity, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
