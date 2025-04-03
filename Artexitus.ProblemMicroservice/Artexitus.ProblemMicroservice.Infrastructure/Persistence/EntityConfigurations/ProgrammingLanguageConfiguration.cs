using Artexitus.ProblemMicroservice.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artexitus.ProblemMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class ProgrammingLanguageConfiguration : IEntityTypeConfiguration<ProgrammingLanguage>
    {
        public void Configure(EntityTypeBuilder<ProgrammingLanguage> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Name)
                .HasMaxLength(255);

            builder
                .Property(p => p.Description)
                .HasMaxLength(7000);
        }
    }
}
