using Artexitus.IdentityMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
        }
    }
}
