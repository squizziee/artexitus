using Artexitus.IdentityMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;

namespace Artexitus.IdentityMicroservice.Infrastructure.Persistence
{
    public class IdentityDatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var now = DateTimeOffset.UtcNow;

            modelBuilder.Entity<UserRole>()
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
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        Name = "Admin",
                        Description =
                            "Has right to every action possible except " +
                            "those that are dangerous to system integrity",
                        CreatedAt = now,
                    },
                    new UserRole
                    {
                        Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                        Name = "ARTSYS",
                        Description =
                            "Preferred not to use directly. Should be " +
                            "used as an authorization blocker to certain endpoints",
                        CreatedAt = now,
                    }
                );

            modelBuilder.Entity<UserProfile>()
                .HasData(
                    new UserProfile
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        Username = "sirgideon",
                        RoleId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        CreatedAt = now,
                        LastUpdatedAt = now,
                    },
                    new UserProfile
                    {
                        Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                        Username = "sys",
                        RoleId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                        CreatedAt = now,
                        LastUpdatedAt = now,
                    }
                );

            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "admin0@artexitus.com",
                        CreatedAt = DateTimeOffset.Now,
                        IsActivated = true,
                        PasswordHash = "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y",
                        ProfileId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        RefreshToken = "0000"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "sys@artexitus.com",
                        CreatedAt = DateTimeOffset.Now,
                        IsActivated = true,
                        PasswordHash = "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y",
                        ProfileId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                        RefreshToken = "0000"
                    }
                );
        }
    }
}
