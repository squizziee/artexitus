using Artexitus.IdentityMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artexitus.IdentityMicroservice.Infrastructure.Persistence.EntityConfiguration
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);

            //builder
            //    .HasQueryFilter(u => u.DeletedAt == null);
            
            builder
                .Property(u => u.Email)
                .HasMaxLength(255);

            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            builder
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey("User");
        }
    }
}
