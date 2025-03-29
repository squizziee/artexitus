namespace Artexitus.IdentityMicroservice.Domain.Repositories
{
    public interface ISearchableRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> SearchAsync(ISpecification<TEntity> specification);
        Task<IPaginatedEnumerable<TEntity>> SearchPaginatedAsync(
            ISpecification<TEntity> specification, int pageNumber, int pageSize);
    }
}
