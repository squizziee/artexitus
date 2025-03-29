using Artexitus.IdentityMicroservice.Domain.Entities;

namespace Artexitus.IdentityMicroservice.Application.Interfaces
{
    public interface IUserRepository : IRepository<User>, IPaginatableRepository<User>, 
        ISearchableRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<User?> GetByStaleEmailAsync(string email, CancellationToken cancellationToken);
        Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
        Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task<User?> GetByActivationTokenAsync(string activationToken, CancellationToken cancellationToken);
    }
}
