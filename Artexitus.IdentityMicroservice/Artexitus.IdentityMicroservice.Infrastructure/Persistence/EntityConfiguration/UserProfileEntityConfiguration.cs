using Artexitus.IdentityMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artexitus.IdentityMicroservice.Infrastructure.Persistence.EntityConfiguration
{
    public class UserProfileEntityConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {

            builder
               .HasKey(p => p.Id);

            builder
                .Property(p => p.Username)
                .HasMaxLength(255);

            builder
                .HasIndex(p => p.Username)
                .IsUnique();

            builder
                .HasOne(p => p.Role)
                .WithMany(r => r.UserProfiles)
                .HasForeignKey("UserProfile");
        }
    }
}
