using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Artexitus.IdentityMicroservice.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDatabaseContext _context;

        public UserRepository(IdentityDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User entity, CancellationToken cancellationToken)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _context.Users.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(User entity, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == entity.Id, cancellationToken);

            if (user == null)
            {
                throw new ResourceDoesNotExistException($"User entity with ID {entity.Id} does not exist. Unable to delete");
            }

            _context.Users.Remove(entity);
        }

        public Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(
                _context.Users
                    .Include(u => u.Profile)
                        .ThenInclude(p => p.Role)
                    .AsEnumerable()
            );
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.Profile)
                    .ThenInclude(p => p.Role)
                .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SoftDeleteAsync(User entity, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == entity.Id, cancellationToken);

            if (user == null)
            {
                throw new ResourceDoesNotExistException($"User entity with ID {entity.Id} does not exist. Unable to soft delete");
            }

            user.DeletedAt = DateTime.UtcNow;
        }

        public async Task UpdateAsync(User entity, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == entity.Id, cancellationToken);

            if (user == null)
            {
                throw new ResourceDoesNotExistException($"User entity with ID {entity.Id} does not exist. Unable to update");
            }

            user.Email = entity.Email;
            user.PasswordHash = entity.PasswordHash;
            user.LastUpdatedAt = DateTime.UtcNow;
        }
    }
}
