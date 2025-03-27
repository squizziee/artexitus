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

        public async Task<User?> GetByActivationTokenAsync(string activationToken, CancellationToken cancellationToken)
        {
            return await _context.Users
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Include(u => u.Profile)
                    .ThenInclude(p => p.Role)
                    .IgnoreQueryFilters()
                .SingleOrDefaultAsync(u => u.ActivationToken == activationToken, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Profile)
                    .ThenInclude(p => p.Role)
                .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Profile)
                    .ThenInclude(p => p.Role)
                .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Profile)
                    .ThenInclude(p => p.Role)
                .SingleOrDefaultAsync(u => u.RefreshToken == refreshToken, cancellationToken);
        }

        public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Profile)
                    .ThenInclude(p => p.Role)
                .SingleOrDefaultAsync(u => u.Profile.Username == username, cancellationToken);
        }

        public Task<IPaginatedEnumerable<User>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber < 0 || pageNumber * pageSize > _context.Users.Count())
            {
                throw new ArgumentOutOfRangeException($"Page number {pageNumber} is invalid");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException($"Page size {pageSize} is invalid");
            }

            var chunk = _context.Users
                .AsNoTracking()
                .Include(u => u.Profile)
                    .ThenInclude(p => p.Role)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .AsEnumerable();

            var totalPages = _context.Users.Count() / pageSize;

            if (_context.Users.Count() % pageSize > 0)
            {
                ++totalPages;
            }

            IPaginatedEnumerable<User> result = new PaginatedEnumerable<User>
            {
                Items = chunk,
                PageNumber = pageNumber,
                TotalPages = totalPages
            };

            return Task.FromResult(result);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<IEnumerable<User>> SearchAsync(ISpecification<User> specification, 
            bool ignoreFilters = false)
        {
            IEnumerable<User> result;

            if (ignoreFilters)
            {
                result = _context.Users
                    .IgnoreQueryFilters()
                    .Include(u => u.Profile)
                        .ThenInclude(p => p.Role)
                    .Where(u => specification.IsSatisfiedBy(u))
                    .AsEnumerable();
            } else
            {
                result = _context.Users
                    .Include(u => u.Profile)
                        .ThenInclude(p => p.Role)
                    .Where(u => specification.IsSatisfiedBy(u))
                    .AsEnumerable();
            }

            return Task.FromResult(result);
        }

        public Task<IPaginatedEnumerable<User>> SearchPaginatedAsync(ISpecification<User> specification, 
            int pageNumber, int pageSize, bool ignoreFilters = false)
        {
            IEnumerable<User> chunk;

            if (ignoreFilters)
            {
                chunk = _context.Users
                    .IgnoreQueryFilters()
                    .Include(u => u.Profile)
                        .ThenInclude(p => p.Role)
                    .Where(u => specification.IsSatisfiedBy(u))
                    .Skip(pageNumber * pageSize)
                    .Take(pageSize)
                    .AsEnumerable();
            }
            else
            {
                chunk = _context.Users
                    .Include(u => u.Profile)
                        .ThenInclude(p => p.Role)
                    .Where(u => specification.IsSatisfiedBy(u))
                    .Skip(pageNumber * pageSize)
                    .Take(pageSize)
                    .AsEnumerable();
            }

            var totalPages = _context.Users.Count() / pageSize;

            if (totalPages % pageSize > 0)
            {
                ++totalPages;
            }

            IPaginatedEnumerable<User> result = new PaginatedEnumerable<User>
            {
                Items = chunk,
                PageNumber = pageNumber,
                TotalPages = totalPages
            };

            return Task.FromResult(result);
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
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(u => u.Id == entity.Id, cancellationToken);

            if (user == null)
            {
                throw new ResourceDoesNotExistException($"User entity with ID {entity.Id} does not exist. Unable to update");
            }

            user.Email = entity.Email;
            user.PasswordHash = entity.PasswordHash;
            user.LastUpdatedAt = DateTime.UtcNow;
            user.RefreshToken = entity.RefreshToken;
            user.ActivationToken = entity.ActivationToken;
            user.IsActivated = entity.IsActivated;
            user.ProfileId = entity.ProfileId;
        }
    }
}
