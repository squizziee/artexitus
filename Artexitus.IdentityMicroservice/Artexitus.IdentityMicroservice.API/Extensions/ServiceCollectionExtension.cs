using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using System.Reflection;
using Artexitus.IdentityMicroservice.API.Filters;

namespace Artexitus.IdentityMicroservice.API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = configuration["Jwt:AccessToken:Issuer"],
                            ValidAudience = configuration["Jwt:AccessToken:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:AccessToken:Key"]!))
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                context.Request.Cookies.TryGetValue("accessToken", out var accessToken);

                                if (!string.IsNullOrEmpty(accessToken))
                                {
                                    context.Token = accessToken;
                                }

                                return Task.CompletedTask;
                            }
                        };
                    }
                );

            services.AddAuthorizationBuilder()
                .AddPolicy("DefaultPolicy", policy => policy.RequireRole("Basic", "Admin", "Author", "ARTSYS"))
                .AddPolicy("AuthorPolicy", policy => policy.RequireRole("Admin", "Author", "ARTSYS"))
                .AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin", "ARTSYS"))
                .AddPolicy("Reserved", policy => policy.RequireRole("ARTSYS"));

            return services;
        }

        public static IServiceCollection AddAutomatedRequestValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<AutomatedValidattionFilter>();
            services.AddControllers(
                options =>
                {
                    options.Filters.Add<AutomatedValidattionFilter>();
                }
            );

            return services;
        }
    }
}
