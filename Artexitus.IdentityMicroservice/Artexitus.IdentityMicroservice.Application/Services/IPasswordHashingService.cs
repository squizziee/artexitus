namespace Artexitus.IdentityMicroservice.Application.Services
{
    public interface IPasswordHashingService
    {
        string HashPassword(string password);
        bool ValidatePassword(string password, string validHash);
    }
}
