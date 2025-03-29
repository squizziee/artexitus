using Artexitus.IdentityMicroservice.Domain.Entities;

namespace Artexitus.IdentityMicroservice.Application.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken(User user);
        (string, DateTimeOffset) GenerateActivationToken(User user);
    }
}
