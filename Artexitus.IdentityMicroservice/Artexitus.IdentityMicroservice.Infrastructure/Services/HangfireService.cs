using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Application.Services;
using Artexitus.IdentityMicroservice.Infrastructure.Specifications;

namespace Artexitus.IdentityMicroservice.Infrastructure.Services
{
    public class HangfireService : IBackgroundJobService
    {
        private readonly IUserRepository _userRepository;

        public HangfireService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async void ClearNonActivatedAccounts()
        {
            var result = await _userRepository
                .SearchAsync(new AccountIsNotActivatedSpecification(), true);

            foreach (var account in result)
            {
                await _userRepository.DeleteAsync(account, CancellationToken.None);
            }

            await _userRepository.SaveChangesAsync(CancellationToken.None);
        }

        public async void DeactivateStaleAccounts()
        {
            var result = await _userRepository
                .SearchAsync(new AccountIsStaleSpecification(358), true);

            foreach (var account in result)
            {
                await _userRepository.SoftDeleteAsync(account, CancellationToken.None);
            }

            await _userRepository.SaveChangesAsync(CancellationToken.None);
        }
    }
}
