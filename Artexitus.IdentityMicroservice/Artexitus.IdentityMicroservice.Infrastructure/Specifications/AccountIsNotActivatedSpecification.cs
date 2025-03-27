using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Domain.Entities;

namespace Artexitus.IdentityMicroservice.Infrastructure.Specifications
{
    public class AccountIsNotActivatedSpecification : ISpecification<User>
    {
        public bool IsSatisfiedBy(User entity)
        {
            return !entity.IsActivated;
        }
    }
}
