using Artexitus.ProblemMicroservice.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artexitus.ProblemMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .HasOne(s => s.Problem)
                .WithMany(p => p.Submissions)
                .HasForeignKey("ProblemId");

            builder
                .HasOne(s => s.Language)
                .WithMany(p => p.Submissions)
                .HasForeignKey("LanguageId");

            builder
                .Property(s => s.SourceCode)
                .HasMaxLength(7000);
        }
    }
}
