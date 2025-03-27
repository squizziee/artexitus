namespace Artexitus.IdentityMicroservice.Application.Interfaces
{
    public interface ISearchableRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> SearchAsync(ISpecification<TEntity> specification, bool ignoreFilters = false);
        Task<IPaginatedEnumerable<TEntity>> SearchPaginatedAsync(
            ISpecification<TEntity> specification, int pageNumber, int pageSize, bool ignoreFilters = false);
    }
}
