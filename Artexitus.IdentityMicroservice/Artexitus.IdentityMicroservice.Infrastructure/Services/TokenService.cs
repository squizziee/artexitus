using Artexitus.IdentityMicroservice.Application.Services;
using Artexitus.IdentityMicroservice.Domain.Entities;
using Artexitus.IdentityMicroservice.Infrastructure.ConfigurationSections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Artexitus.IdentityMicroservice.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly AccessTokenSettings _accessTokenSettings;
        private readonly RefreshTokenSettings _refreshTokenSettings;
        private readonly ActivationTokenSettings _activationTokenSettings;

        public TokenService(IConfiguration configuration,
            IOptions<AccessTokenSettings> accessTokenSettings,
            IOptions<RefreshTokenSettings> refreshTokenSettings,
            IOptions<ActivationTokenSettings> activationTokenSettings)
        {
            _accessTokenSettings = accessTokenSettings.Value;
            _refreshTokenSettings = refreshTokenSettings.Value;
            _activationTokenSettings = activationTokenSettings.Value;
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new Claim[]
            {
                new (ClaimTypes.Name, user.Id.ToString()),
                new (ClaimTypes.Email, user.Email),
                new (ClaimTypes.Role, user.Profile.Role.Name),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _accessTokenSettings.Issuer,
                audience: _accessTokenSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_accessTokenSettings.ExpirationTimeInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public (string, DateTimeOffset) GenerateActivationToken(User user)
        {
            var claims = new Claim[]
           {
                new (ClaimTypes.Name, user.Id.ToString()),
                new (ClaimTypes.Email, user.Email),
                new (ClaimTypes.Role, user.Profile.Role.Name),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_activationTokenSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var validTo = DateTime.UtcNow.AddHours(_activationTokenSettings.ExpirationTimeInHours);

            var token = new JwtSecurityToken(
                issuer: _activationTokenSettings.Issuer,
                audience: _activationTokenSettings.Audience,
                claims: claims,
                expires: validTo,
                signingCredentials: creds
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), validTo);
        }

        public string GenerateRefreshToken(User user)
        {
            var claims = new Claim[]
            {
                new (ClaimTypes.Name, user.Id.ToString()),
                new (ClaimTypes.Email, user.Email),
                new (ClaimTypes.Role, user.Profile.Role.Name),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshTokenSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _refreshTokenSettings.Issuer,
                audience: _refreshTokenSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_refreshTokenSettings.ExpirationTimeInDays),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
