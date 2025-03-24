using Artexitus.IdentityMicroservice.Application.Interfaces;

namespace Artexitus.IdentityMicroservice.Infrastructure.Persistence.Specifications
{
    public class AndSpecification<TEntity> (ISpecification<TEntity> left, ISpecification<TEntity> right)
        : ISpecification<TEntity> where TEntity : class
    {
        public bool IsSatisfiedBy(TEntity entity)
        {
            return left.IsSatisfiedBy(entity) && right.IsSatisfiedBy(entity);
        }
    }
}
