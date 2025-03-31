namespace Artexitus.IdentityMicroservice.Contracts.Helpers
{
    public class UserTokens
    {
        public required string AccessToken { get; init; }
        public required string RefreshToken { get; init; }
    }
}
