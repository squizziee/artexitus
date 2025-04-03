namespace Artexitus.ProblemMicroservice.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IPaginatableRepository<TEntity> where TEntity : class
    {
        Task<IPaginatedEnumerable<TEntity>> GetPaginatedAsync(int pageNumber, int pageSize,
            CancellationToken cancellationToken);
    }
}
