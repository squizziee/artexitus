namespace Artexitus.ProblemMicroservice.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IPaginatedEnumerable<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Items { get; set; }
        int PageNumber { get; set; }
        int TotalPages { get; set; }
    }
}
