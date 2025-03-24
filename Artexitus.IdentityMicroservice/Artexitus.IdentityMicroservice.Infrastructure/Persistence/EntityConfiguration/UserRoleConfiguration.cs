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
                .HasQueryFilter(u => u.DeletedAt == null);

            builder
                .Property(r => r.Name)
                .HasMaxLength(255);

            builder
                .HasIndex(r => r.Name)
                .IsUnique();

            builder
                .Property(r => r.Description)
                .HasMaxLength(7000);

            var now = DateTimeOffset.UtcNow;

            builder
                .HasData(
                    new UserRole
                    {
                        Id = Guid.NewGuid(),
                        Name = "Basic",
                        Description = "Normal user",
                        CreatedAt = now,
                    },
                    new UserRole
                    {
                        Id = Guid.NewGuid(),
                        Name = "Author",
                        Description =
                            "Problem author. Has every right " +
                            "of the normal user and can create problems",
                        CreatedAt = now,
                    },
                    new UserRole
                    {
                        Id = Guid.NewGuid(),
                        Name = "Admin",
                        Description =
                            "Has right to every action possible except " +
                            "those that are dangerous to system integrity",
                        CreatedAt = now,
                    },
                    new UserRole
                    {
                        Id = Guid.NewGuid(),
                        Name = "ARTSYS",
                        Description =
                            "Preferred not to use directly. Should be " +
                            "used as an authorization blocker to certain endpoints",
                        CreatedAt = now,
                    }
                );
        }
    }
}
