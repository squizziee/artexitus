using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Artexitus.IdentityMicroservice.Infrastructure.Specifications
{
    public class AccountIsNotActivatedSpecification : BaseSpecification<User>
    {
        public AccountIsNotActivatedSpecification()
        {
            Criteria = (user => !user.IsActivated && user.ActivationTokenValidTo < DateTimeOffset.UtcNow);
            IgnoreQueryFilters = true;
            IsEvaluatedOnClientSide = true;
        }
    }
}
