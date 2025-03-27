using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace Artexitus.IdentityMicroservice.Infrastructure.Specifications
{
    public class AccountIsStaleSpecification : ISpecification<User>
    {
        private readonly int _daysUntilStale;

        public AccountIsStaleSpecification(int daysUntilStale)
        {
            _daysUntilStale = daysUntilStale;
        }

        public bool IsSatisfiedBy(User entity)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(entity.RefreshToken);

            if (jsonToken is not JwtSecurityToken token)
            {
                return false;
            }

            return token.ValidTo.AddDays(_daysUntilStale) < DateTimeOffset.Now;
        }
    }
}
