using Artexitus.IdentityMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artexitus.IdentityMicroservice.Infrastructure.Persistence.EntityConfiguration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder
               .HasKey(r => r.Id);

            builder
                .Property(r => r.Name)
                .HasMaxLength(255);

            builder
                .HasIndex(r => r.Name)
                .IsUnique();

            builder
                .Property(r => r.Description)
                .HasMaxLength(7000);
        }
    }
}
