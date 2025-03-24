using Artexitus.IdentityMicroservice.Application.Interfaces;

namespace Artexitus.IdentityMicroservice.Infrastructure.Persistence.Repositories
{
    public class PaginatedEnumerable<TEntity> : IPaginatedEnumerable<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Items { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
