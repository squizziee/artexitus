using Artexitus.IdentityMicroservice.Application.Services;

namespace Artexitus.IdentityMicroservice.Infrastructure.Services
{
    public class PasswordHashingService : IPasswordHashingService
    {
        public string HashPassword(string password)
        {
            var hash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);

            return hash;
        }

        public bool ValidatePassword(string password, string validHash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, validHash);
        }
    }
}
