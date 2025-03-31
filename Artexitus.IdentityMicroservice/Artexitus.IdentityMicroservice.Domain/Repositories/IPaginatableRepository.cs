using Artexitus.IdentityMicroservice.Domain.Repositories;

namespace Artexitus.IdentityMicroservice.Domain.Repositories
{
    public interface IPaginatableRepository<TEntity> where TEntity : class
    {
        Task<IPaginatedEnumerable<TEntity>> GetPaginatedAsync(int pageNumber, int pageSize,
            CancellationToken cancellationToken);
    }
}
