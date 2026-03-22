using Microsoft.EntityFrameworkCore;
using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Persitence.Context;

namespace SchoolAppSol.Persitence.Repositories
{
    public sealed class EnrollmentStatusRepository : IEnrollmentStatusDomainRepository
    {
        private readonly SchoolContext _context;

        public EnrollmentStatusRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<bool> CodeExistsAsync(string code, int? excludingId, CancellationToken ct = default)
        {
            return await _context.EnrollmentStatuses.AsNoTracking()
                .AnyAsync(es => es.Name == code && !es.Deleted && (!excludingId.HasValue || es.Id != excludingId.Value), ct);
        }

        public async Task<bool> ExistsActiveAsync(int enrollmentStatusId, CancellationToken ct = default)
        {
            return await _context.EnrollmentStatuses.AsNoTracking()
                .AnyAsync(es => es.Id == enrollmentStatusId && !es.Deleted, ct);
        }

        public async Task<bool> ExistsAsync(int enrollmentStatusId, CancellationToken ct = default)
        {
            return await _context.EnrollmentStatuses.AsNoTracking()
                .AnyAsync(es => es.Id == enrollmentStatusId, ct);
        }
    }
}
