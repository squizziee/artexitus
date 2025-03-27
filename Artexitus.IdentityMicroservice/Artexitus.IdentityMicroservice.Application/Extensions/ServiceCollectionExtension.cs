using Artexitus.IdentityMicroservice.Application.ConfigurationSections;
using Artexitus.IdentityMicroservice.Application.Services;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Artexitus.IdentityMicroservice.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaginationSettings>(configuration.GetSection("Pagination"));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => 
                config.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly)
            );

            //RecurringJob.AddOrUpdate<IBackgroundJobService>(
            //    "clear-non-activated-accounts", 
            //    s => s.ClearNonActivatedAccounts(), Cron.Minutely()
            //);

            //RecurringJob.AddOrUpdate<IBackgroundJobService>(
            //    "clear-stale-accounts",
            //    s => s.DeactivateStaleAccounts(), Cron.Daily()
            //);

            return services;
        }
    }
}
