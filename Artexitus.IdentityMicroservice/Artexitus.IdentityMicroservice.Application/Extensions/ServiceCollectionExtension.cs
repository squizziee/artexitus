using Microsoft.Extensions.DependencyInjection;

namespace Artexitus.IdentityMicroservice.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => 
                config.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly)
            );

            return services;
        }
    }
}
