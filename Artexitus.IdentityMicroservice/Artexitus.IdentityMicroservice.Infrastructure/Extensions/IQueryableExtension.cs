using Artexitus.IdentityMicroservice.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Artexitus.IdentityMicroservice.Infrastructure.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec) where T : class
        {

            var queryableResultWithIncludes = spec.Includes
                .Aggregate(query,
                    (current, include) => current.Include(include));

            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            if (spec.IgnoreQueryFilters)
            {
                secondaryResult = secondaryResult.IgnoreQueryFilters();
            }

            if (spec.IsEvaluatedOnClientSide)
            {
                secondaryResult = secondaryResult
                    .AsEnumerable()
                    .Where(_ => true)
                    .AsQueryable();
            }

            return secondaryResult.Where(spec.Criteria);
        }
    }
}
