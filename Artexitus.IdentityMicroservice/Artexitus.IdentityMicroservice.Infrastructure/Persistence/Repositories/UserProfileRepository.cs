using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Artexitus.IdentityMicroservice.Infrastructure.Persistence.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly IdentityDatabaseContext _context;

        public UserProfileRepository(IdentityDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserProfile entity, CancellationToken cancellationToken)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _context.UserProfiles.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(UserProfile entity, CancellationToken cancellationToken)
        {
            var profile = await _context.UserProfiles
                .SingleOrDefaultAsync(u => u.Id == entity.Id, cancellationToken);

            if (profile == null)
            {
                throw new ResourceDoesNotExistException($"User profile entity with ID {entity.Id} does not exist. Unable to delete");
            }

            _context.UserProfiles.Remove(entity);
        }

        public Task<IEnumerable<UserProfile>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(
                _context.UserProfiles
                    .Include(p => p.Role)
                    .AsEnumerable()
            );
        }

        public async Task<UserProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.UserProfiles
                .Include(p => p.Role)
                .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SoftDeleteAsync(UserProfile entity, CancellationToken cancellationToken)
        {
            var profile = await _context.UserProfiles
               .SingleOrDefaultAsync(u => u.Id == entity.Id, cancellationToken);

            if (profile == null)
            {
                throw new ResourceDoesNotExistException($"User profile entity with ID {entity.Id} does not exist. Unable to soft delete");
            }

            profile.DeletedAt = DateTime.UtcNow;
        }

        public async Task UpdateAsync(UserProfile entity, CancellationToken cancellationToken)
        {
            var profile = await _context.UserProfiles
                .SingleOrDefaultAsync(u => u.Id == entity.Id, cancellationToken);

            if (profile == null)
            {
                throw new ResourceDoesNotExistException($"User profile entity with ID {entity.Id} does not exist. Unable to update");
            }

            profile.RoleId = entity.RoleId;
            profile.Username = entity.Username;
            profile.LastUpdatedAt = DateTime.UtcNow;
        }
    }
}
