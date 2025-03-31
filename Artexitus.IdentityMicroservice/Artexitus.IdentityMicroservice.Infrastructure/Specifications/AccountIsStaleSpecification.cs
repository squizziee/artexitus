using Artexitus.IdentityMicroservice.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Artexitus.IdentityMicroservice.Infrastructure.Specifications
{
    public class AccountIsStaleSpecification : BaseSpecification<User>
    {

        public AccountIsStaleSpecification(int daysUntilStale)
        {
            Criteria = (user => user.LastRefresh.AddDays(daysUntilStale) < DateTimeOffset.UtcNow);
        }
    }
}
