using Artexitus.ProblemMicroservice.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artexitus.ProblemMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class SuccessfulSubmissionConfiguration : IEntityTypeConfiguration<SuccesfulSubmission>
    {
        public void Configure(EntityTypeBuilder<SuccesfulSubmission> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .HasOne(ss => ss.Submission)
                .WithOne()
                .HasForeignKey("SuccesfulSubmission", "SubmissionId");
        }
    }
}
