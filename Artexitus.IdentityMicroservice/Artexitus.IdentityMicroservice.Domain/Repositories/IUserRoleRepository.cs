using Artexitus.IdentityMicroservice.Domain.Entities;

namespace Artexitus.IdentityMicroservice.Domain.Repositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<UserRole> GetDefaultRoleAsync(CancellationToken cancellationToken);
    }
}
