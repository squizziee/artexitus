using Artexitus.IdentityMicroservice.Application.Interfaces;
using Artexitus.IdentityMicroservice.Application.Services;
using Artexitus.IdentityMicroservice.Infrastructure.ConfigurationSections;
using Artexitus.IdentityMicroservice.Infrastructure.Persistence;
using Artexitus.IdentityMicroservice.Infrastructure.Persistence.Repositories;
using Artexitus.IdentityMicroservice.Infrastructure.Services;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Artexitus.IdentityMicroservice.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddInfrastructureConfigSections(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<AccessTokenSettings>(configuration.GetSection("Jwt:AccessToken"));
			services.Configure<RefreshTokenSettings>(configuration.GetSection("Jwt:RefreshToken"));
			services.Configure<ActivationTokenSettings>(configuration.GetSection("Jwt:ActivationToken"));
			services.Configure<EmailSettings>(configuration.GetSection("Email"));

			return services;
		}

		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<IdentityDatabaseContext>(
				options =>
				{
					options.UseSqlServer(
						configuration.GetConnectionString("IdentityMicroserviceDatabaseConnectionString")
					);
                }
			);

			services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("RedisConnectionString");
                options.InstanceName = "Identity_";
            });


            services.AddScoped<IPasswordHashingService, PasswordHashingService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddSingleton<IEmailService, EmailService>();

			services.AddScoped<ICacheAccessor, RedisCacheAccessor>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserProfileRepository, UserProfileRepository>();
			services.AddScoped<IUserRoleRepository, UserRoleRepository>();

			return services;
		}

		public static IServiceCollection AddBackgroundJobs(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddHangfire(
				config =>
                {
					config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
					config.UseSimpleAssemblyNameTypeSerializer();
					config.UseSerializerSettings(
						new JsonSerializerSettings
						{
							ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
						}
					);
                    config.UseSqlServerStorage
					(
						configuration.GetConnectionString("HangfireDatabaseConnectionString"), 
						new SqlServerStorageOptions
						{
							PrepareSchemaIfNecessary = true
						}
					);
				}
			);

			services.AddHangfireServer();

			return services;
		}
	}
}
