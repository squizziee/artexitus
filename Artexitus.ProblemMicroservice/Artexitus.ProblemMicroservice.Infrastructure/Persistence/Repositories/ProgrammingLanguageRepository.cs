using Artexitus.ProblemMicroservice.Contracts.Exceptions;
using Artexitus.ProblemMicroservice.Infrastructure.Entities;
using Artexitus.ProblemMicroservice.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artexitus.ProblemMicroservice.Infrastructure.Persistence.Repositories
{
    public class ProgrammingLanguageRepository : IProgrammingLanguageRepository
    {
        private readonly ProblemDatabaseContext _context;

        public ProgrammingLanguageRepository(ProblemDatabaseContext context)
        {
            _context = context;
        }
        
        public async Task AddAsync(ProgrammingLanguage entity, CancellationToken cancellationToken)
        {
            await _context.Languages.AddAsync(entity, cancellationToken);
        }

        public Task DeleteAsync(ProgrammingLanguage entity, CancellationToken cancellationToken)
        {
            _context.Languages.Remove(entity);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<ProgrammingLanguage>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(
                _context.Languages.AsEnumerable()
            );
        }

        public async Task<ProgrammingLanguage?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _context.Languages
                .Where(l => l.Id == id)
                .SingleOrDefaultAsync();

            return result;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(ProgrammingLanguage entity, CancellationToken cancellationToken)
        {
            var language = await _context.Languages
                .SingleOrDefaultAsync(l => l.Id == entity.Id, cancellationToken);

            if (language == null)
            {
                throw new ResourceDoesNotExistException($"Language entity with ID {entity.Id} does not exist. Unable to soft delete");
            }

            language.DeletedAt = DateTimeOffset.UtcNow;
        }

        public async Task UpdateAsync(ProgrammingLanguage entity, CancellationToken cancellationToken)
        {
            var language = await _context.Languages
                .SingleOrDefaultAsync(l => l.Id == entity.Id, cancellationToken);

            if (language == null)
            {
                throw new ResourceDoesNotExistException($"Language entity with ID {entity.Id} does not exist. Unable to update");
            }

            language.Name = entity.Name;
            language.Description = entity.Description;
            language.LastUpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
