namespace Artexitus.IdentityMicroservice.Application.Interfaces
{
    public interface ISpecification<TEntity> where TEntity : class
    {
        bool IsSatisfiedBy(TEntity entity);
    }
}
