using Artexitus.IdentityMicroservice.Application.Services;
using Hangfire;

namespace Artexitus.IdentityMicroservice.API.Misc
{
    public static class RecurringJobInstantiator
    {
        public static void Init()
        {
            RecurringJob.AddOrUpdate<IBackgroundJobService>(
                "clear-non-activated-accounts",
                s => s.ClearNonActivatedAccounts(), Cron.Daily()
            );

            RecurringJob.AddOrUpdate<IBackgroundJobService>(
                "clear-stale-accounts",
                s => s.DeactivateStaleAccounts(), Cron.Daily()
            );
        }
    }
}
