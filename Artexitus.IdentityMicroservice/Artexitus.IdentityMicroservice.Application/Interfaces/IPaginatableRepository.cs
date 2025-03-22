using Artexitus.IdentityMicroservice.Domain.Entities;

namespace Artexitus.IdentityMicroservice.Application.Interfaces
{
    public interface IPaginatableRepository<TEntity> where TEntity : class
    {
        Task<IPaginatedEnumerable<TEntity>> GetPaginatedAsync(int pageNumber, int pageSize, 
            CancellationToken cancellationToken);
    }
}
