using Artexitus.IdentityMicroservice.Application.Behaviors;
using Artexitus.IdentityMicroservice.Application.ConfigurationSections;
using MediatR;
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
            services.Configure<PasswordResetSettings>(configuration.GetSection("PasswordReset"));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => 
                config.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly)
            );

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
