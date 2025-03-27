using Artexitus.IdentityMicroservice.Application.ConfigurationSections;
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

            return services;
        }
    }
}
