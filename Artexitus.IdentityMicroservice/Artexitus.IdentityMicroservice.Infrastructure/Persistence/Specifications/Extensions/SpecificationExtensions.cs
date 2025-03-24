using Artexitus.IdentityMicroservice.Application.Interfaces;

namespace Artexitus.IdentityMicroservice.Infrastructure.Persistence.Specifications.Extensions
{
    public static class SpecificationExtensions
    {
        public static ISpecification<T> And<T>(this ISpecification<T> first, ISpecification<T> second) where T : class
        {
            return new AndSpecification<T>(first, second);
        }

        public static ISpecification<T> Or<T>(this ISpecification<T> first, ISpecification<T> second) where T : class
        {
            return new OrSpecification<T>(first, second);
        }

        public static ISpecification<T> Not<T>(this ISpecification<T> specification) where T : class
        {
            return new NotSpecification<T>(specification);
        }
    }
}
