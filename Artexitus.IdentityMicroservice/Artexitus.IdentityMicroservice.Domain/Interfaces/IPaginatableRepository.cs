using Artexitus.IdentityMicroservice.Domain.Entities;

namespace Artexitus.IdentityMicroservice.Domain.Interfaces
{
    public interface IPaginatableRepository<TEntity> where TEntity : EntityBase
    {
        Task<IPaginatedEnumerable<TEntity>> GetPaginatedAsync(int pageNumber, int pageSize, 
            CancellationToken cancellationToken);
    }
}
