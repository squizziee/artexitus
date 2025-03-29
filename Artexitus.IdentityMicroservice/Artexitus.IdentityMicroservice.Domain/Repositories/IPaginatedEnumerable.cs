using Artexitus.IdentityMicroservice.Domain.Entities;

namespace Artexitus.IdentityMicroservice.Domain.Repositories
{
    public interface IPaginatedEnumerable<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Items { get; set; }
        int PageNumber { get; set; }
        int TotalPages { get; set; }
    }
}
