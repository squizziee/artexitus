using Artexitus.ProblemMicroservice.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artexitus.ProblemMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class GeneralProblemStatisticsConfiguration : IEntityTypeConfiguration<GeneralProblemStatistics>
    {
        public void Configure(EntityTypeBuilder<GeneralProblemStatistics> builder)
        {
            builder
                .HasKey(gps => gps.Id);

            builder
                .HasOne(gps => gps.Problem)
                .WithOne(p => p.GeneralStatistics)
                .HasForeignKey("GeneralProblemStatistics", "ProblemId");
        }
    }
}
