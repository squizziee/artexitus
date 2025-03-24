using Artexitus.IdentityMicroservice.Application.Interfaces;

namespace Artexitus.IdentityMicroservice.Infrastructure.Persistence.Specifications
{
    public class NotSpecification<TEntity>(ISpecification<TEntity> left)
        : ISpecification<TEntity> where TEntity : class
    {
        public bool IsSatisfiedBy(TEntity entity)
        {
            return !left.IsSatisfiedBy(entity);
        }
    }
}
