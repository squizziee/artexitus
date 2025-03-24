namespace Artexitus.IdentityMicroservice.Contracts.Helpers
{
    public struct UserTokens
    {
        public required string AccessToken { get; init; }
        public required string RefreshToken { get; init; }
    }
}
