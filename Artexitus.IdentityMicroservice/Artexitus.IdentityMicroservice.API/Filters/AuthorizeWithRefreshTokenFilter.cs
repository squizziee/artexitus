using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Infrastructure.ConfigurationSections;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Artexitus.IdentityMicroservice.API.Filters
{
    public class AuthorizeWithRefreshTokenFilter : IAuthorizationFilter
    {
        private readonly RefreshTokenSettings _refreshTokenSettings;

        public AuthorizeWithRefreshTokenFilter(IOptions<RefreshTokenSettings> options)
        {
            _refreshTokenSettings = options.Value;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var refreshToken = context.HttpContext.Request.Cookies
                .FirstOrDefault(c => c.Key == "refreshToken");

            if (refreshToken.Equals(default(KeyValuePair<string, string>)))
            {
                throw new InvalidCredentialsException("Refresh token is not present in request cookies");
            }

            TokenValidationParameters validationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidIssuer = _refreshTokenSettings.Issuer,
                ValidAudience = _refreshTokenSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshTokenSettings.Key)),
            };

            var validator = new JwtSecurityTokenHandler();

            if (!validator.CanReadToken(refreshToken.Value))
            {
                throw new InvalidCredentialsException("Refresh token is not present in request cookies");
            }

            try
            {
                validator.ValidateToken(refreshToken.Value, validationParameters, out _);
            }
            catch
            {
                throw new InvalidCredentialsException("Refresh token is not present in request cookies");
            }
        }
    }
}
