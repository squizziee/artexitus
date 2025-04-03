using Artexitus.ProblemMicroservice.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artexitus.ProblemMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class ProblemConfiguration : IEntityTypeConfiguration<Problem>
    {
        public void Configure(EntityTypeBuilder<Problem> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Name)
                .HasMaxLength(255);

            builder
                .Property(p => p.MarkdownDescription)
                .HasMaxLength(7000);

            builder
                .HasMany(p => p.SupportedLanguages)
                .WithMany(l => l.ProblemsWithSupport)
                .UsingEntity<ProblemStarterCode>(
                    builder =>
                    {
                        builder
                            .Property(psc => psc.SourceCode)
                            .HasMaxLength(7000);
                    }           
                );

            builder
                .HasMany(p => p.Statistics)
                .WithMany(l => l.ProblemsWithStatistics)
                .UsingEntity<ProblemStatistics>(
                    builder =>
                    {
                        builder
                            .OwnsMany("ProblemStatisticsBin", "DistributionByTimeElapsed", b =>
                            {
                                b.ToJson();
                            });

                        builder
                            .OwnsMany("ProblemStatisticsBin", "DistributionBySpaceElapsed", b =>
                            {
                                b.ToJson();
                            });
                    }
                );
        }
    }
}
