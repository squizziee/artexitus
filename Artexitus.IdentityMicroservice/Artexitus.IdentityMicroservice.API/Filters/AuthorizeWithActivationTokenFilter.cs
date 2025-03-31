using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Infrastructure.ConfigurationSections;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Artexitus.IdentityMicroservice.API.Filters
{
    public class AuthorizeWithActivationTokenFilter : IAuthorizationFilter
    {
        private readonly ActivationTokenSettings _activationTokenSettings;

        public AuthorizeWithActivationTokenFilter(IOptions<ActivationTokenSettings> options)
        {
            _activationTokenSettings = options.Value;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var activationToken = context.HttpContext.Request.Query
                .FirstOrDefault(p => p.Key == "ActivationToken");

            var exception = new InvalidCredentialsException("Activation token is not present in request query");

            if (activationToken.Equals(default(KeyValuePair<string, string>)))
            {
                throw exception;
            }

            TokenValidationParameters validationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidIssuer = _activationTokenSettings.Issuer,
                ValidAudience = _activationTokenSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_activationTokenSettings.Key)),
            };

            var validator = new JwtSecurityTokenHandler();

            if (!validator.CanReadToken(activationToken.Value))
            {
                throw exception;
            }

            try
            {
                validator.ValidateToken(activationToken.Value, validationParameters, out _);
            }
            catch
            {
                throw exception;
            }
        }
    }
}
