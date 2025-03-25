namespace Artexitus.IdentityMicroservice.Contracts.DTO
{
    public record PaginatedResponse<TEntity>
    {
        public required IEnumerable<TEntity> Items { get; init; }
        public required int PageNumber { get; init; }
        public required int TotalPages { get; init; }
    }
}
