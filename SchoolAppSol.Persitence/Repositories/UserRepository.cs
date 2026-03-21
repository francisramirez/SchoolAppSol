using Microsoft.EntityFrameworkCore;
using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Persitence.Context;

namespace SchoolAppSol.Persitence.Repositories
{
    public sealed class UserRepository : IUserRepository, IUserDomainRepository
    {
        private readonly SchoolContext _context;

        public UserRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User entity, CancellationToken ct = default)
        {
            await _context.Users.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
        {
            return await _context.Users.AsNoTracking()
                .AnyAsync(u => u.Email.ToLower() == email.ToLower() && !u.Deleted, ct);
        }

        public async Task<bool> ExistsActiveAsync(int userId, CancellationToken ct = default)
        {
            return await _context.Users.AsNoTracking()
                .AnyAsync(u => u.UserId == userId && !u.Deleted, ct);
        }

        public async Task<IReadOnlyList<UserModel>> GetAllActiveAsync(CancellationToken ct = default)
        {
            return await _context.Users.AsNoTracking()
                .Where(u => !u.Deleted)
                .Select(u => new UserModel
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email
                })
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Users.AsNoTracking()
                .Where(u => !u.Deleted)
                .ToListAsync(ct);
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id && !u.Deleted, ct);
        }

        public async Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default)
        {
            return await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower() && !u.Deleted, ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id, ct);
            if (user != null)
            {
                user.Deleted = true;
                user.DeletedDate = DateTime.UtcNow;
                user.UserDeleted = userId;
                _context.Users.Update(user);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task UpdateAsync(User entity, CancellationToken ct = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == entity.UserId, ct);
            
            if (user == null)
                throw new Persitence.Exceptions.PersistenceException("El usuario no fue encontrado.");

            user.Username = entity.Username;
            user.Email = entity.Email;
            user.PasswordHash = entity.PasswordHash;
            user.ModifyDate = entity.ModifyDate;
            user.UserMod = entity.UserMod;

            _context.Users.Update(user);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> UsernameExistsAsync(string username, CancellationToken ct = default)
        {
            return await _context.Users.AsNoTracking()
                .AnyAsync(u => u.Username.ToLower() == username.ToLower() && !u.Deleted, ct);
        }
    }
}
