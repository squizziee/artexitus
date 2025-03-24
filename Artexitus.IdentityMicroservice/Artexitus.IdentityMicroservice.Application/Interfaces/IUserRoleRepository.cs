using Artexitus.IdentityMicroservice.Domain.Entities;

namespace Artexitus.IdentityMicroservice.Application.Interfaces
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<UserRole> GetDefaultRoleAsync(CancellationToken cancellationToken);
    }
}
