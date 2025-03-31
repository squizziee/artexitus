namespace Artexitus.IdentityMicroservice.Application.ConfigurationSections
{
    public record PaginationSettings
    {
        public required int UsersPageSize { get; init; }
    }
}
