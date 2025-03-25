using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Application.Services;
using Artexitus.IdentityMicroservice.Infrastructure.ConfigurationSections;
using Artexitus.IdentityMicroservice.Infrastructure.Persistence;
using Artexitus.IdentityMicroservice.Infrastructure.Persistence.Repositories;
using Artexitus.IdentityMicroservice.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Artexitus.IdentityMicroservice.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureConfigSections(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AccessTokenSettings>(configuration.GetSection("Jwt:AccessToken"));
            services.Configure<RefreshTokenSettings>(configuration.GetSection("Jwt:RefreshToken"));
            services.Configure<ActivationTokenSettings>(configuration.GetSection("Jwt:ActivationToken"));

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDatabaseContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("IdentityMicroserviceDatabaseConnectionString")
                )
            );

            services.AddScoped<IPasswordHashingService, PasswordHashingService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();

            return services;
        }
    }
}
