using Artexitus.IdentityMicroservice.Application.Services;
using Artexitus.IdentityMicroservice.Infrastructure.Extensions;
using Artexitus.IdentityMicroservice.Infrastructure.Persistence;
using Artexitus.IdentityMicroservice.Infrastructure.Specifications;

namespace Artexitus.IdentityMicroservice.Infrastructure.Services
{
    public class HangfireService : IBackgroundJobService
    {
        private readonly IdentityDatabaseContext _context;

        public HangfireService(IdentityDatabaseContext context)
        {
            _context = context;
        }

        public void ClearNonActivatedAccounts()
        {
            var result = _context.Users
                .Specify(new AccountIsNotActivatedSpecification())
                .AsEnumerable();

            foreach (var account in result)
            {
                _context.Users.Remove(account);
            }

            _context.SaveChanges();
        }

        public async void DeactivateStaleAccounts()
        {
            //var result = await _userRepository
            //    .SearchAsync(new AccountIsStaleSpecification(365 * 5));

            //foreach (var account in result)
            //{
            //    await _userRepository.SoftDeleteAsync(account, CancellationToken.None);
            //}

            //await _userRepository.SaveChangesAsync(CancellationToken.None);
        }
    }
}
