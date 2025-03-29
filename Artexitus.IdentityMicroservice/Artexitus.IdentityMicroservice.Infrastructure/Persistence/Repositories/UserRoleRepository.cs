using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Artexitus.IdentityMicroservice.Infrastructure.Persistence.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IdentityDatabaseContext _context;

        public UserRoleRepository(IdentityDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserRole entity, CancellationToken cancellationToken)
        {
            entity.CreatedAt = DateTimeOffset.UtcNow;
            await _context.UserRoles.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(UserRole entity, CancellationToken cancellationToken)
        {
            var role = await _context.UserRoles
                .SingleOrDefaultAsync(u => u.Id == entity.Id, cancellationToken);

            if (role == null)
            {
                throw new ResourceDoesNotExistException($"User role entity with ID {entity.Id} does not exist. Unable to delete");
            }

            _context.UserRoles.Remove(entity);
        }

        public Task<IEnumerable<UserRole>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(
                _context.UserRoles.AsEnumerable()
            );
        }

        public async Task<UserRole?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.UserRoles
                .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<UserRole> GetDefaultRoleAsync(CancellationToken cancellationToken)
        {
            return await _context.UserRoles
                .SingleAsync(r => r.Name == "Basic", cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SoftDeleteAsync(UserRole entity, CancellationToken cancellationToken)
        {
            var role = await _context.UserRoles
               .SingleOrDefaultAsync(u => u.Id == entity.Id, cancellationToken);

            if (role == null)
            {
                throw new ResourceDoesNotExistException($"User role entity with ID {entity.Id} does not exist. Unable to soft delete");
            }

            role.DeletedAt = DateTimeOffset.UtcNow;
        }

        public async Task UpdateAsync(UserRole entity, CancellationToken cancellationToken)
        {
            var role = await _context.UserRoles
                .SingleOrDefaultAsync(u => u.Id == entity.Id, cancellationToken);

            if (role == null)
            {
                throw new ResourceDoesNotExistException($"User role entity with ID {entity.Id} does not exist. Unable to update");
            }

            role.Name = entity.Name;
            role.Description = entity.Description;
            role.LastUpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
