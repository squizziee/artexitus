using Artexitus.IdentityMicroservice.Domain.Entities;

namespace Artexitus.IdentityMicroservice.Application.Services
{
    public interface IEmailService
    {
        Task SendAccountActivationEmail(User user, CancellationToken cancellationToken);
    }
}
