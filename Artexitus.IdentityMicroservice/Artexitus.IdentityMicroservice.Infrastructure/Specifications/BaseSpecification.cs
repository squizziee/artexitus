using Artexitus.IdentityMicroservice.Domain.Repositories;
using System.Linq.Expressions;

namespace Artexitus.IdentityMicroservice.Infrastructure.Specifications
{
    public abstract class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];
        public List<string> IncludeStrings { get; } = [];
        public bool IgnoreQueryFilters { get; set;}
        public bool IsEvaluatedOnClientSide { get; set;}

        protected virtual void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}
