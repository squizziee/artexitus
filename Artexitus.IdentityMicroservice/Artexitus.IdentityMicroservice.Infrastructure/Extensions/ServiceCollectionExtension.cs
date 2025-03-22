using Artexitus.IdentityMicroservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Artexitus.IdentityMicroservice.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDatabaseContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("IdentityMicroserviceDatabaseConnectionString")
                )
            );

            return services;
        }
    }
}
